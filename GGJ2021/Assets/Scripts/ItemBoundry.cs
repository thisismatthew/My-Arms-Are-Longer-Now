using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoundry : MonoBehaviour
{
    public GameObject SceneLoader;
    public GameObject item;
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
            //cut back to the main scene
            SceneLoader.GetComponent<SceneLoader>().CutBackToMainScene();
            //put the money back in place
            collision.transform.position = startPos;
        }
    }
}
