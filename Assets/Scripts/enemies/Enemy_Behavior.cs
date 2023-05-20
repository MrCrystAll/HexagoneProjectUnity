using System;
using System.Collections;
using System.Collections.Generic;
using model.enemies;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy_Behavior : MonoBehaviour
{
    public bool hasTurned = false;
    
    public Enemy Enemy;
    private Slider _healthBar;

    public event EventHandler<EventArgs> Death;

    protected virtual void Start()
    {
        hasTurned = false;
        _healthBar = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward.normalized * (Time.deltaTime * Enemy.Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Trigger>(out var t) && !hasTurned)
        {
            transform.Rotate(0, t.yRotation, 0);
            _healthBar.gameObject.transform.rotation = Quaternion.Euler(90, 90, 0);
            hasTurned = true;
        }
        
        if (other.gameObject.CompareTag("Base"))
        {
            OnDeath();
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Trigger>(out var t))
        {
            hasTurned = false;
        }
    }

    public void DealDamage(float turretDamage)
    {
        Enemy.Health -= turretDamage;
        _healthBar.value = Enemy.Health / Enemy.TotalHealth;
        if (Enemy.Health <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        Death?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
