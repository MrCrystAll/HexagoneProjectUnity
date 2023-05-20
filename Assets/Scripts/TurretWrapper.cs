using System;
using System.Collections;
using System.Collections.Generic;
using model.allies.turrets;
using UnityEngine;

public class TurretWrapper : MonoBehaviour
{
    public Hitter turretModel;
    public TurretBehaviour Canon { get; set; }
    public TurretPosition Position { get; set; }

    private void Start()
    {
        //Pas beau, mais seul moyen pour régler une null reference
        turretModel = GetComponent<TurretGraphic>() switch
        {
            TurretLv1Graphic _ => new TurretLv1(),
            TurretLv2Graphic _ => new TurretLv2(),
            TurretLv3Graphic _ => new TurretLv3(),
            _ => turretModel
        };
        Canon.Turret = turretModel;
    }
}
