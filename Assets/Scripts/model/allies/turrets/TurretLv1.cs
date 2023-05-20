namespace model.allies.turrets
{
    public class TurretLv1 : Hitter
    {
        public TurretLv1()
        {
            Damage = 8;
            RoF = 2;
            Range = 5;
            UpgradeCost = 150;
            BaseCost = 150;
        }
    }
}