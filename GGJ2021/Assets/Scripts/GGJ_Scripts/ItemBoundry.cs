using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoundry : MonoBehaviour
{
    public GameObject SceneLoader;
    public GameObject item;
    public GameObject hand;
    public GameObject macroHand;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = item.transform.position;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            //hand.GetComponentInChildren<SpriteRenderer>().sprite = hand.GetComponent<Hand>().Open_img;
            item.transform.parent = null;
            hand.GetComponent<Hand>().itemHeld = false;
            collision.transform.parent = null;
            //put the money back in place
            item.GetComponent<Rigidbody2D>().MovePosition(startPos);
            collision.transform.position = startPos;
            macroHand.GetComponent<Arm>().hasMoney = true;
            FindObjectOfType<AudioManager>().Play("retract");
            //cut back to the main scene
            SceneLoader.GetComponent<SceneLoader>().CutBackToMainScene();

        }
    }
}
