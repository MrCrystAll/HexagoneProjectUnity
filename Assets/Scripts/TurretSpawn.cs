using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretSpawn : MonoBehaviour
{

    private TurretPosition[] _turretPositions;
    // Start is called before the first frame update
    void Start()
    {
        _turretPositions = gameObject.GetComponentsInChildren<TurretPosition>();
    }
    
    public void StartGlowingAll()
    {
        foreach (var turretPosition in _turretPositions)
        {
            turretPosition.StartGlowing();
        }
    }

    public GameObject GetClosestObjectWithin(Vector3 position, float distance)
    {
        GameObject min_object = null;
        float min_distance = distance;
        foreach (var turretPosition in _turretPositions)
        {
            if ((turretPosition.transform.position - position).magnitude < min_distance && turretPosition.IsFree)
            {
                min_object = turretPosition.gameObject;
                min_distance = (turretPosition.transform.position - position).magnitude;
            }
        }

        return min_object;
    }
    
    public void EndGlowingAll()
    {
        print("End of glowing for all");
        foreach (var turretPosition in _turretPositions)
        {
            turretPosition.EndGlowing();
        }
    }
}
