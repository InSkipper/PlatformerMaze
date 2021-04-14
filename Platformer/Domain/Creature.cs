namespace Platformer.Domain
{
    public class Creature
    {
        public readonly int MaxHealth;
        public readonly int MaxStamina;

        public Creature(int maxHealth, int maxStamina, Point initialPosition)
        {
            MaxHealth = maxHealth;
            MaxStamina = maxStamina;
            Position = initialPosition;
        }

        public Point Position { get; set; }
    }
}