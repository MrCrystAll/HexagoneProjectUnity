using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public float speed;
    private Image _image;

    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        speed = speed == 0 ? float.Epsilon : speed;
        _image = GetComponent<Image>();
        StartCoroutine(Fade());
    }
    
    IEnumerator Fade()
    {
        float fadeAmount = _image.color.a - speed * Time.deltaTime;
        
        while (_image.color.a < 1)
        {
            fadeAmount = _image.color.a + speed * Time.deltaTime;
            _image.color = new Color(0, 0, 0, fadeAmount);
            yield return new WaitForFixedUpdate();
        }

        if (sceneName != string.Empty)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
            
        
        gameObject.SetActive(false);
    }
}
