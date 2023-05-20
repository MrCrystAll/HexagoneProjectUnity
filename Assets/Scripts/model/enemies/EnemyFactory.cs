namespace model.enemies
{
    public static class EnemyFactory
    {
        public static Enemy CreateLv1Enemy()
        {
            return new Enemy(5, 10, 5, 50);
        }
        
        public static Enemy CreateLv2Enemy()
        {
            return new Enemy(2, 40, 10, 100);
        }
        
        public static Enemy CreateLv3Enemy()
        {
            return new Enemy(7, 25, 15, 200);
        }
    }
}