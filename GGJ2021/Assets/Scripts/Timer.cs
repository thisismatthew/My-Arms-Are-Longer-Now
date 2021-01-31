using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeLimit = 4f;
    public float soundTimeLimit = 2f;
    private float time;
    public string nextSceneTitle;
    public bool gameEnd = false;
    private bool soundPlayed = false;
    public string soundName;

    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;
        if (time > timeLimit)
        {
            if (gameEnd)
            {
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene(nextSceneTitle);
            }
                
        }
        
        if (time > soundTimeLimit && soundName!= null)
        {
            if (!soundPlayed)
            {
                FindObjectOfType<AudioManager>().Play(soundName);
                soundPlayed = true;
            }
        }

    }
}
