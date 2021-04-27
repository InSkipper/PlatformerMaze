using FluentAssertions;
using NUnit.Framework;
using Platformer.Domain;

namespace ModelTests
{
    [TestFixture]
    class PlayerTests
    {
        private Player player = new Player();
        private float deltaTime = 1 / 60f;

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void MovePlayer()
        {
            player.MoveHorizontally(6.33f);
            player.PosX.Should().Be(6.33f * deltaTime);
        }

        [Test]
        public void Player_ShouldStay_WhenCollide()
        {
            player.MoveVertically(6);
            player.PosY.Should().Be(0);
        }
    }
}
