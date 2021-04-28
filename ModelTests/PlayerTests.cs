using FluentAssertions;
using NUnit.Framework;
using Platformer.Domain;

namespace ModelTests
{
    [TestFixture]
    class PlayerTests
    {
        private Player player;
        private float deltaTime = 1 / 60f;

        [SetUp]
        public void SetUp()
        {
            Game.CurrentMap = Map.FromLines(Game.TestMap);
            player = Game.CurrentMap.Player;
        }

        [Test]
        public void MovePlayer()
        {
            player.MoveHorizontally(6.33f);
            player.PosX.Should().Be(Game.CurrentMap.InitialPosition.X + 6.33f * deltaTime);
        }

        [Test]
        public void Player_ShouldStay_WhenCollide()
        {
            player.MoveVertically(6);
            player.MoveHorizontally(-1);
            player.PosX.Should().Be(Game.CurrentMap.InitialPosition.X + 0);
            player.PosY.Should().Be(Game.CurrentMap.InitialPosition.Y + 0);
        }
    }
}
