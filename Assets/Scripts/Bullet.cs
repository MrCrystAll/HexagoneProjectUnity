using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _timeBeforeDisappear = .1f;
    private ParticleSystem _particle;

    private void Start()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        _timeBeforeDisappear -= Time.deltaTime;
        if (_timeBeforeDisappear <= 0)
        {
            Destroy(gameObject);
        }
    }
}
