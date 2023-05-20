namespace model.allies.turrets
{
    public class TurretLv2 : Hitter
    {
        public TurretLv2()
        {
            Damage = 9.5f;
            RoF = 2.5f;
            Range = 6;
            UpgradeCost = 250;
            BaseCost = 300;
        }
    }
}