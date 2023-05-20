using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretsAvailableAnim : MonoBehaviour
{
    public Animator animator;
    private bool _isUp = true;

    private GameObject _tutorial;

    private void Start()
    {
        _tutorial = GameObject.Find("Tutorial");
    }

    public void WantsToSwitch()
    {
        if (_tutorial.activeSelf) return;
        animator.SetTrigger(_isUp ? "OnDown" : "OnUp");

        _isUp = !_isUp;
    }


}
