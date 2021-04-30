using FluentAssertions;
using NUnit.Framework;
using Platformer.Domain;

namespace ModelTests
{
    [TestFixture]
    class PlayerTests
    {
        private Game game = new Game(Map.FromLines(Game.TestMap));
        private Player player;
        private float deltaTime = 1 / 60f;

        [SetUp]
        public void SetUp()
        {
            player = game.Player;
        }

        [Test]
        public void MovePlayer()
        {
            player.MakeMove(6.33f, 0);
            player.PosX.Should().Be(game.CurrentMap.InitialPosition.X + 6.33f * deltaTime);
        }

        [Test]
        public void Player_ShouldStay_WhenCollide()
        {
            player.MakeMove(0, 6);
            player.PosX.Should().Be(game.CurrentMap.InitialPosition.X + 0);
        }
    }
}
