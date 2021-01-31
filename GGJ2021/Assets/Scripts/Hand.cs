using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject item;
    public GameObject pinchPoint;
    public GameObject SceneLoader;
    public Sprite pinch_img;
    public Sprite open_img;
    private bool touchingItem;
    public bool itemHeld;
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    public Sprite Open_img { get => open_img; set => open_img = value; }

    void FixedUpdate()
    {
        if (!SceneLoader.GetComponent<SceneLoader>().InMainScene)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            //transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
            gameObject.GetComponent<Rigidbody2D>().MovePosition(mousePosition);
        }
    }

    private void Update()
    {
        if (!SceneLoader.GetComponent<SceneLoader>().InMainScene)
        {
            if (touchingItem && Input.GetMouseButtonDown(0))
            {
                GetComponentInChildren<SpriteRenderer>().sprite = pinch_img;
                itemHeld = true;
            }
            if (itemHeld && Input.GetMouseButtonUp(0))
            {
                GetComponentInChildren<SpriteRenderer>().sprite = open_img;
                item.transform.parent = null;
                itemHeld = false;
            }
            if (itemHeld)
            {
                item.GetComponent<Rigidbody2D>().MovePosition(pinchPoint.transform.position);
                item.transform.position = pinchPoint.transform.position;
            }
        }



        //i wanted to do this with colliders and 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Item")
        {
            GetComponentInChildren<SpriteRenderer>().sprite = open_img;
            touchingItem = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            touchingItem = false;
        }
    }

}
