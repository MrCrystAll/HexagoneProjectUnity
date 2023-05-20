using model.allies.turrets;
using model.enemies;
using UnityEngine;

public static class ExtensionMethods
{

    public static Object Instantiate(this Object thisObj, Object original, Vector3 position, Quaternion rotation, Hitter turret)
    {
        GameObject turretGo = Object.Instantiate(original, position, rotation) as GameObject;
        var upgradeButton = turretGo.GetComponent<TurretWrapper>();
        upgradeButton.turretModel = turret;
        return turretGo;
    }

    public static Object Instantiate(this Object thisObj, Object original, Vector3 position, Quaternion rotation,
        Enemy enemy)
    {
        GameObject enemyGo = Object.Instantiate(original, position, rotation) as GameObject;
        var enemyComp = enemyGo.GetComponent<Enemy_Behavior>();
        enemyComp.Enemy = enemy;
        return enemyGo;
    }
}