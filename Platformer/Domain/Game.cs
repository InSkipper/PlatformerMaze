namespace Platformer.Domain
{
    public class Game
    {
        public GameStage GameStage { get; set; }
        public static Map CurrentMap;

        public Game(Map currentMap)
        {
            CurrentMap = currentMap;
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
    }
}
