﻿namespace Platformer.Domain
{
    public class Game
    {
        public GameStage GameStage { get; set; }
        public Map CurrentMap { get; set; }
        public Player Player { get; private set; }
        public Camera Camera { get; set; }

        public Game(Map currentMap)
        {
            CurrentMap = currentMap;
            Player = new Player(currentMap);
            Camera = new Camera(currentMap, Player);
        }

        public void ChangeMap(Map map)
        {
            CurrentMap = map;
            Player = new Player(map);
            Camera = new Camera(map, Player);
        }

        public static string[] TestMap =
        {
            "###################",
            "#.................#",
            "#P....#.......#...#",
            "########..###...#.#",
            ".......#.##...##..#",
            "............###...#",
            ".......#######....#",
            "##############....#",
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
            "#.................#",
            "#..A..............#",
            "#.................#",
            "#.................#",
            "#.................#",
            "###################",
        };

        public static string[] LargeMap =
        {
            "###...##########################" ,
            "###..##........................." ,
            "###.##.........................." ,
            "#####..........................." ,
            "###............................." ,
            "#........................#.....#" ,
            "#........................#.....#" ,
            "#...###..###.............#.....#" ,
            "#.....#..#.......P.......####..#" ,
            "#.....#..#.############........#" ,
            "#........#.#############.......#" ,
            "####.....#######################" ,
        };
    }
}
