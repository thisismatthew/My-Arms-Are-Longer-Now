using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject item;
    public GameObject SceneLoader;
    private bool touchingItem;
    private bool itemHeld;
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    void FixedUpdate()
    {
        if (!SceneLoader.GetComponent<SceneLoader>().InMainScene)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }
    }

    private void Update()
    {
        if (!SceneLoader.GetComponent<SceneLoader>().InMainScene)
        {
            if (touchingItem && Input.GetMouseButtonDown(0))
            {
                item.transform.parent = transform;
                itemHeld = true;
            }
            if (itemHeld && Input.GetMouseButtonUp(0))
            {
                item.transform.parent = null;
            }
        }

        //i wanted to do this with colliders and 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Item")
        {
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
