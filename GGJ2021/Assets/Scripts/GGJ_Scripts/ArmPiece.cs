using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArmPiece : MonoBehaviour
{

    private float time = 0f;

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

    private void Update()
    {
        //slight ripple effect to the arm pieces so that they feel a bit more human and organic
        time += Time.deltaTime;
        GameObject sprite = this.transform.GetChild(0).gameObject;
        Vector3 spritePos = sprite.transform.localPosition;
        spritePos.y = 0.1f * Mathf.Sin(time * 1.5f);
        sprite.transform.localPosition = spritePos;
    }
}
