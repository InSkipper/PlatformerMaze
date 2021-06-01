using System;
using System.IO;

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
                if (Math.Abs(Player.PosX - CurrentMap.End.X) < 0.3 && Math.Abs(Player.PosY - CurrentMap.End.Y) < 0.3)
                    GameStage = GameStage.FinishedMap;
                foreach (var enemy in CurrentMap.Enemies)
                {
                    enemy.TargetX = Player.PosX;
                    enemy.TargetY = Player.PosY;
                    enemy.MoveToTarget(deltaTime);
                    if (Math.Abs(Player.PosX - enemy.PosX) < 0.6f
                        && Math.Abs(Player.PosY - enemy.PosY) < 0.6f)
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
            "#.....O#######....#",
            "###################",
        };

        public static string[] Level4 =
        {
            "#################...#...#...#.....E#",
            "###...............#...#...#...######",
            "###.##########.#####################",
            "#P......E##....###################E#",
            "##..####.##..#.###################.#",
            "###.#O...#...#.............E.....#.#",
            "E##.#####....#.#############...#.#.#",
            ".#...........#.#...........###.#.#.#",
            "...###.#######.#######.#.#...#.#.#.#",
            "######.#...............#.###.#.#.#.#",
            "#....#.#.###############.....#.#.#.#",
            "#..#...#.....#.........###.#...#.#.#",
            "#..#####.....#.###########.#####.#.#",
            "#.E................................#",
            "####################################",

        };

        public static string[] MapWithoutWalls =
        {
            "###################",
            "#P................#",
            "#.....E...........#",
            "#..E..E...........#",
            "#.................#",
            "#.................#",
            "#O................#",
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
            "#.O...#..#.############........#" ,
            "#........#.#############.......#" ,
            "################################" ,
        };
    }
}
