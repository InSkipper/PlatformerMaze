using System.Collections.Generic;
using System.Linq;

namespace Platformer.Domain
{
    public class Map
    {
        public readonly int Width;
        public readonly int Height;

        private HashSet<Creature> enemies = new HashSet<Creature>();

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public List<Platform> Platforms { get; set; }

        public void AddEnemy(Creature enemy)
        {
            enemies.Add(enemy);
        }

        public IReadOnlyList<Creature> GetEnemies()
        {
            return enemies.ToList();
        }
    }
}