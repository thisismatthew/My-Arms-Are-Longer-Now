using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArmPiece : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "gaurd")
        {
            FindObjectOfType<AudioManager>().Stop("stretch");
            FindObjectOfType<AudioManager>().Stop("retract");

            FindObjectOfType<AudioManager>().Play("gameover");
            SceneManager.LoadScene("GameOver");
            //triggerGameover
        }
    }
}
