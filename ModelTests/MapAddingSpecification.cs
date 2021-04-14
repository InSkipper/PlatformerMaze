using FluentAssertions;
using NUnit.Framework;
using Platformer.Domain;

namespace ModelTests
{
    [TestFixture]
    class MapAddingSpecification
    {
        private Map map;
        private Creature player;
        private Creature enemy;
        private Creature strongEnemy;
        private Point initialPosition;

        [SetUp]
        public void SetUp()
        {
            initialPosition = new Point(1, 1);
            map = new Map(10, 10);
            player = new Creature(100, 100, initialPosition);
            enemy = new Creature(10, 10, initialPosition);
            strongEnemy = new Creature(100, 50, initialPosition);
        }

        [Test]
        public void GetEnemies_ShouldReturnNothing_WhenNotAdded()
        {
            map.GetEnemies().Should().BeEmpty();
        }

        [Test]
        public void GetEnemies_ShouldReturnAddedEnemies()
        {
            map.AddEnemy(enemy);
            map.AddEnemy(strongEnemy);
            map.GetEnemies().Should().BeEquivalentTo(enemy, strongEnemy);
        }

        [Test]
        public void GetEnemies_ShouldNotReturnDuplicates()
        {
            map.AddEnemy(enemy);
            map.AddEnemy(enemy);
            map.AddEnemy(strongEnemy);
            map.GetEnemies().Should().BeEquivalentTo(enemy, strongEnemy);
        }
    }
}
