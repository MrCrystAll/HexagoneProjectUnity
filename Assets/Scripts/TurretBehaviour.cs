using System;
using System.Collections;
using System.Collections.Generic;
using model.allies.turrets;
using model.enemies;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    public Hitter Turret { get; set; }
    public TurretPosition TurretPos { get; set; }
    public bool Active { get; set; }

    public GameObject bulletPrefab;

    private float _lastHitTimer;

    RaycastHit? TryGetRaycast()
    {
        var increment = 10;
        for (int angle = 0; angle < 360; angle += increment)
        {
            var point = new Vector3(transform.position.x, 0.5f, transform.position.z) + Quaternion.Euler(0, angle + increment, 0) * Vector3.forward;

            Ray r = new Ray(new Vector3(transform.position.x, 0.5f, transform.position.z), (new Vector3(transform.position.x, 0.5f, transform.position.z) - point).normalized);
            if (Physics.Raycast(r, out var hit, Turret.Range) && hit.collider.CompareTag("Enemy"))
            {
                return hit;
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active) return;
        _lastHitTimer = Mathf.Max(_lastHitTimer - Time.deltaTime, 0);
        var r = TryGetRaycast();
        HandleShooting(r);
        
        //DrawCircle(transform.position, 5);
    }

    private void HandleShooting(RaycastHit? ray)
    {
        if (!ray.HasValue) return;
        transform.LookAt(ray.Value.collider.transform);
        if (_lastHitTimer > 0) return;

        _lastHitTimer = 1f / Turret.RoF;

        Enemy_Behavior enemy = ray.Value.collider.gameObject.GetComponent<Enemy_Behavior>();

        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 5000f);

        //Debug.DrawLine(transform.position, ray.Value.collider.transform.position, Color.green, 1);
        enemy.DealDamage(Turret.Damage);
    }
}
