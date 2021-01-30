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

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            SceneLoader.GetComponent<SceneLoader>().CutBackToMainScene();
            collision.transform.position = startPos;
        }
    }
}
