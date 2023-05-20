using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Base : MonoBehaviour
{

    public event EventHandler<EventArgs> Damaged;

    private float _health;

    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            OnDamaged();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        
        var enemy = other.GetComponent<Enemy_Behavior>();
        Health -= enemy.Enemy.Damage;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnDamaged()
    {
        Damaged?.Invoke(this, EventArgs.Empty);
    }
}
