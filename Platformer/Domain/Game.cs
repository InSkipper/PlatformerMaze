using System;

namespace Platformer.Domain
{
    public class Game
    {
        public GameStage GameStage { get; set; }
        public Map CurrentMap { get; set; }
        public Creature Player { get; private set; }
        public Camera Camera { get; set; }

        private DateTime lastUpdate = DateTime.MinValue;

        public Game(Map currentMap)
        {
            CurrentMap = currentMap;
            Player = new Player(currentMap);
            Camera = new Camera(currentMap, Player);
            foreach (var enemy in currentMap.Enemies)
            {
                enemy.Map = currentMap;
            }
        }

        public void ChangeMap(Map map)
        {
            CurrentMap = map;
            Player = new Player(map);
            Camera = new Camera(map, Player);
            foreach (var enemy in map.Enemies)
            {
                enemy.Map = map;
            }
        }

        public void Update()
        {
            var now = DateTime.Now;
            var deltaTime = (float)(now - lastUpdate).TotalMilliseconds / 1000f;
            if (lastUpdate != DateTime.MinValue)
            {
                Player.MakeMove(deltaTime);
                foreach (var enemy in CurrentMap.Enemies)
                {
                    enemy.TargetX = Player.PosX;
                    enemy.TargetY = Player.PosY;
                    enemy.MoveToTarget(deltaTime);
                    if (Math.Abs(Player.PosX - enemy.PosX) < Map.TileSize
                        && Math.Abs(Player.PosY - enemy.PosY) < Map.TileSize)
                        Player.IsDead = true;
                }
            }
            lastUpdate = now;
        }

        public static string[] TestMap =
        {
            "###################",
            "#.................#",
            "#P....#.......#...#",
            "########..###...#.#",
            "#..E.....##...##..#",
            "#...........###...#",
            "#......#######....#",
            "###################",
        };

        public static string[] TestMap2 =
        {
            "###################",
            "#####P###......####",
            "#...#.....#..#.#..#",
            "#...#####.#..#.##.#",
            "#...###.#.#######.#",
            "#...##.........##.#",
            "#.......######....#",
            "###################",
        };

        public static string[] MapWithoutWalls =
        {
            "###################",
            "#P................#",
            "#.....E...........#",
            "#..E..E...........#",
            "#.................#",
            "#.................#",
            "#.................#",
            "###################",
        };

        public static string[] LargeMap =
        {
            "###...##########################" ,
            "###..##........................#" ,
            "###.##.........................#" ,
            "#####..........................#" ,
            "###............................#" ,
            "#........................#.....#" ,
            "#........................#.....#" ,
            "#...###..###.............#.....#" ,
            "#.....#..#.......P.E.....####..#" ,
            "#.....#..#.############........#" ,
            "#........#.#############.......#" ,
            "################################" ,
        };
    }
}
