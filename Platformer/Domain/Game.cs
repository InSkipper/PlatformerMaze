namespace Platformer.Domain
{
    public class Game
    {
        public GameStage GameStage { get; set; }
        public Map CurrentMap;

        public Game(Map currentMap)
        {
            CurrentMap = currentMap;
        }
    }
}
