﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{ 
    public bool InMainScene = true;
    public GameObject cam;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("bank");
    }

    public void CutToMiniGame()
    {
        //freeze the main scene functions
        InMainScene = false;
        //move camera to minigame coords
        cam.transform.position = new Vector3(80f, 0f, -10f);
    }

    public void CutBackToMainScene()
    {
        //freeze the main scene functions
        InMainScene = true;
        //move camera to minigame coords
        cam.transform.position = new Vector3(0f, 0f, -10f);
    }
}
