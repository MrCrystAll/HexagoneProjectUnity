using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _money = 500;

    public float Money
    {
        get => _money;
        set
        {
            _money = value;
            OnMoneyChanged();
        }
    }

    public event EventHandler<EventArgs> MoneyChanged;

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnMoneyChanged()
    {
        MoneyChanged?.Invoke(this, EventArgs.Empty);
    }
}
