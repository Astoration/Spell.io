﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScene : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadScene", 3f);    
    }

    public void LoadScene() {
        SceneManager.LoadScene("TitleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
