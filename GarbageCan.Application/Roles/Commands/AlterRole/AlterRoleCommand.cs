﻿using GarbageCan.Application.Common.Interfaces;
using GarbageCan.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GarbageCan.Application.Roles.Commands.AlterRole
{
    public class AlterRoleCommand : IRequest<bool>
    {
        public bool Add { get; set; }
        public ulong ChannelId { get; set; }
        public Emoji Emoji { get; set; }
        public ulong GuildId { get; set; }
        public ulong MessageId { get; set; }
        public ulong UserId { get; set; }
    }

    public class AlterRoleCommandHandler : IRequestHandler<AlterRoleCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<AlterRoleCommandHandler> _logger;
        private readonly IDiscordGuildRoleService _roleService;

        public AlterRoleCommandHandler(IApplicationDbContext context,
            IDiscordGuildRoleService roleService,
            ILogger<AlterRoleCommandHandler> logger)
        {
            _context = context;
            _roleService = roleService;
            _logger = logger;
        }

        public async Task<bool> Handle(AlterRoleCommand request, CancellationToken cancellationToken)
        {
            var roles = await _context.reactionRoles
                .Where(x => x.channelId == request.ChannelId && x.messageId == request.MessageId)
                .ToListAsync(cancellationToken);

            roles = roles.Where(r => r.emoteId == EmoteId(request.Emoji)).ToList();

            foreach (var reactionRole in roles)
            {
                try
                {
                    if (request.Add)
                    {
                        await _roleService.GrantRoleAsync(request.GuildId, reactionRole.roleId, request.UserId, "reaction role");
                        _logger.LogInformation("Granted Role for {@RoleAction}", new { request.GuildId, RoleId = reactionRole.roleId, request.UserId });
                    }
                    else
                    {
                        await _roleService.RevokeRoleAsync(request.GuildId, reactionRole.roleId, request.UserId, "reaction role");
                        _logger.LogInformation("Revoked Role for {@RoleAction}", new { request.GuildId, RoleId = reactionRole.roleId, request.UserId });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Couldn't alter reaction role. Request: {@Request} Role: {@Role}", request, reactionRole);
                }
            }

            return true;
        }

        private static string EmoteId(Emoji emote)
        {
            return emote.Id == 0 ? emote.Name : emote.Id.ToString();
        }
    }
}