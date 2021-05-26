﻿using FluentAssertions;
using NUnit.Framework;
using Platformer.Domain;

namespace ModelTests
{
    [TestFixture]
    class PlayerTests
    {
        private Game game = new Game(Map.FromLines(Game.TestMap));
        private Creature creature;
        private float deltaTime = 1 / 60f;

        [SetUp]
        public void SetUp()
        {
            creature = game.Player;
        }

        [Test]
        public void MovePlayer()
        {
            creature.MakeMove(6.33f, 0);
            creature.PosX.Should().Be(game.CurrentMap.InitialPosition.X + 6.33f * deltaTime);
        }

        [Test]
        public void Player_ShouldStay_WhenCollide()
        {
            creature.MakeMove(0, 6);
            creature.PosX.Should().Be(game.CurrentMap.InitialPosition.X + 0);
        }
    }
}
