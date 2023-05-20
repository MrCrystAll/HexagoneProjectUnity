using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private GameObject _firstPart;

    private GameObject _secondPart;
    // Start is called before the first frame update
    void Start()
    {
        _firstPart = transform.GetChild(0).gameObject;
        _secondPart = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadFirstPart()
    {
        _secondPart.SetActive(false);
        _firstPart.SetActive(true);
    }

    public void LoadSecondPart()
    {
        _firstPart.SetActive(false);
        _secondPart.SetActive(true);
    }

    public void UnloadEverything()
    {
        gameObject.SetActive(false);
    }
}
