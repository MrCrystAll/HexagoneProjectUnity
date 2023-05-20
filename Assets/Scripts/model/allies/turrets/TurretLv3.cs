namespace model.allies.turrets
{
    public class TurretLv3 : Hitter
    {
        public TurretLv3()
        {
            Damage = 12;
            RoF = 3;
            Range = 7;
            UpgradeCost = -1;
            BaseCost = 1000;
        }
    }
}