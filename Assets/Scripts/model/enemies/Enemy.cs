namespace model.enemies
{
    public class Enemy
    {
        public float Speed { get; set; }
        
        public float TotalHealth { get; set; }
        public float Health { get; set; }
        
        public float Damage { get; set; }
        
        public bool SlowedDown { get; set; }
        
        public float MoneyGain { get; set; }

        public Enemy(float speed, float health, float damage, float moneyGain)
        {
            Speed = speed;
            TotalHealth = health;
            Health = health;
            Damage = damage;
            MoneyGain = moneyGain;
        }
    }
}