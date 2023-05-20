namespace model
{
    public class Wave
    {
        public int Level1Amount;
        public int Level2Amount;
        public int Level3Amount;
        
        /// <summary>
        /// Rate of spawn
        /// </summary>
        public float RoS;

        public Wave(int lv1, int lv2, int lv3, float roS)
        {
            Level1Amount = lv1;
            Level2Amount = lv2;
            Level3Amount = lv3;
            RoS = roS;
        }
    }
}