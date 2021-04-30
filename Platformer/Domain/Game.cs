namespace Platformer.Domain
{
    public class Game
    {
        public GameStage GameStage { get; set; }
        public Map CurrentMap { get; set; }
        public Player Player { get; private set; }

        public Game(Map currentMap)
        {
            CurrentMap = currentMap;
            Player = new Player(currentMap);
        }

        public void ChangeMap(Map map)
        {
            CurrentMap = map;
            Player = new Player(map);
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
            "#.................#",
            "#.................#",
            "#.................#",
            "#.................#",
            "###################",
        };
    }
}
