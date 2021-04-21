﻿using FluentAssertions;
using GarbageCan.Application.Common.Interfaces;
using GarbageCan.Application.UnitTests.Shared;
using GarbageCan.Application.XP.Commands.AddXpToUser;
using GarbageCan.Domain.Entities.XP;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace GarbageCan.Application.UnitTests.XP.Commands
{
    public class AddXpToUserCommandTests
    {
        private ApplicationFixture _appFixture;
        private DbContextFixture _contextFixture;
        private IApplicationDbContext _dbContext;
        private IXpCalculatorService _calculator;

        [SetUp]
        public void Setup()
        {
            _appFixture = new ApplicationFixture();
            _contextFixture = new DbContextFixture();
            _dbContext = _contextFixture.MockContext;
            _calculator = Substitute.For<IXpCalculatorService>();

            _appFixture.OnConfigureServices += (_, services) =>
            {
                var service = services
                    .First(descriptor => descriptor.ServiceType == typeof(IXpCalculatorService));
                services.Remove(service);
                services.AddSingleton(_calculator);
                services.AddSingleton(_contextFixture.MockContext);
            };
        }

        [Test]
        public async Task ShouldAddXp_WhenUserExists()
        {
            ulong userId = 90;
            var message = "TEST";
            var user = new User
            {
                Id = userId,
                Lvl = 0,
                XP = 0
            };

            _contextFixture.XPUsers.Add(user);
            _calculator.XpEarned(message).Returns(20.0);

            var command = new AddXpToUserCommand
            {
                UserId = userId,
                Message = message
            };

            await _appFixture.SendAsync(command);

            _contextFixture.XPUsers.First().XP.Should().Be(20);
            _calculator.Received(1).XpEarned(message);

            await _dbContext.Received(1).SaveChangesAsync(default);
        }

        [Test]
        public async Task ShouldCreateNewUser_WhenUserDoesNotExist()
        {
            ulong userId = 90;
            var message = "TEST";

            _calculator.XpEarned(message).Returns(0);

            User addedUser = null;
            _dbContext.XPUsers.When(x => x.Add(Arg.Any<User>())).Do(x => addedUser = x.Arg<User>());

            var command = new AddXpToUserCommand
            {
                UserId = userId,
                Message = message
            };

            await _appFixture.SendAsync(command);

            _dbContext.XPUsers.Received(1).Add(Arg.Any<User>());
            await _dbContext.Received(1).SaveChangesAsync(default);

            addedUser.Should().NotBeNull();
            addedUser.Id.Should().Be(90);
            addedUser.Lvl.Should().Be(0);
            addedUser.XP.Should().Be(0);
        }
    }
}