﻿using GarbageCan.Domain.Entities.Boosters;
using GarbageCan.Domain.Entities.Config;
using GarbageCan.Domain.Entities.Moderation;
using GarbageCan.Domain.Entities.Roles;
using GarbageCan.Domain.Entities.XP;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GarbageCan.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<EntityConditionalRole> ConditionalRoles { get; set; }
        public DbSet<EntityConfig> Config { get; set; }
        public DbSet<EntityJoinRole> JoinRoles { get; set; }
        public DbSet<EntityWatchedUser> JoinWatchlist { get; set; }
        public DbSet<EntityLevelRole> LevelRoles { get; set; }
        public DbSet<EntityActionLog> ModerationActionLogs { get; set; }
        public DbSet<EntityActiveChannelRestrict> ModerationActiveChannelRestricts { get; set; }
        public DbSet<EntityActiveMute> ModerationActiveMutes { get; set; }
        public DbSet<EntityReactionRole> ReactionRoles { get; set; }
        public DbSet<EntityActiveBooster> XPActiveBoosters { get; set; }
        public DbSet<EntityAvailableSlot> XPAvailableSlots { get; set; }
        public DbSet<EntityExcludedChannel> XPExcludedChannels { get; set; }
        public DbSet<EntityQueuedBooster> XPQueuedBoosters { get; set; }
        public DbSet<EntityUserBooster> XPUserBoosters { get; set; }
        public DbSet<EntityUser> XPUsers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}