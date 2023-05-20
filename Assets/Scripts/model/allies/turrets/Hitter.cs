using UnityEngine;

namespace model.allies.turrets
{
    public abstract class Hitter
    {
        /// <summary>
        /// Damage of the weapon
        /// </summary>
        public float Damage { get; set; }
        
        /// <summary>
        /// Rate of fire
        /// </summary>
        public float RoF { get; set; }
        
        /// <summary>
        /// Range of fire
        /// </summary>
        public float Range { get; set; }
        
        /// <summary>
        /// Amount of money necessary to upgrade it, -1 if not upgradable
        /// </summary>
        public float UpgradeCost { get; set; }


        /// <summary>
        /// Amount of money necessary to buy one
        /// </summary>
        public float BaseCost { get; set; }
    }
}