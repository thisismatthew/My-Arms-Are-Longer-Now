using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float time = 10f;
    public string nextSceneTitle;

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            SceneManager.LoadScene(nextSceneTitle);
        }
        
    }
}
