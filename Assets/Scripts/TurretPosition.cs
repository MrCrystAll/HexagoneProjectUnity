using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class TurretPosition : MonoBehaviour
{
    public bool IsFree { get; set; } = true;
    private MeshRenderer _renderer;

    public GameObject turretInstance;

    // Start is called before the first frame update
    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.enabled = false;
    }
    public void StartGlowing()
    {
        if (IsFree)
        {
            _renderer.enabled = true;
            _renderer.material.color = Color.green;
        }
        else
        {
            _renderer.enabled = false;
        }
    }

    public void EndGlowing()
    {
        _renderer.enabled = false;
    }

    public void Replace(GameObject lvturret)
    {
        if (turretInstance)
        {
            Destroy(turretInstance);
        }

        turretInstance = lvturret;
        turretInstance.transform.position = transform.position;
    }
}
