using System;
using System.Collections;
using System.Collections.Generic;
using model;
using model.allies.turrets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropTurret : MonoBehaviour, IPointerExitHandler
{
    public GameObject turret;
    public Hitter Hitter { get; set; }
    public float Price { get; set; }

    private TextMeshProUGUI _priceText;

    private bool _canPay;
    public TurretsAvailableAnim animator;

    public bool CanPay
    {
        get => _canPay;
        set
        {
            _canPay = value;
            _priceText.color = _canPay ? Color.white : Color.red;
        }
    }
    public event EventHandler<Vector2Args> MouseExited;

    private void Start()
    {
        var go = turret.GetComponent<TurretGraphic>();
        animator = GetComponentInParent<TurretsAvailableAnim>();

        //Pas beau
        Hitter = go switch
        {
            TurretLv1Graphic _ => new TurretLv1(),
            TurretLv2Graphic _ => new TurretLv2(),
            TurretLv3Graphic _ => new TurretLv3(),
            _ => Hitter
        };
        Price = Hitter.BaseCost;
        _priceText = GetComponentInChildren<TextMeshProUGUI>();
        _priceText.text = Price.ToString("F0");
    }

    protected virtual void OnMouseExited(Vector2 point)
    {
        MouseExited?.Invoke(this, new Vector2Args(point));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            print(eventData.position);
            OnMouseExited(eventData.position);
        }
        
    }
}
