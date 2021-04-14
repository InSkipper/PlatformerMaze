using System.Collections.Generic;

namespace Platformer.Domain
{
    public class Game
    {
        public GameStage GameStage { get; set; }
        public List<Map> Maps { get; set; }
    }
}
