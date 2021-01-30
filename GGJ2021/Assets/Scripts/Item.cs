using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject SceneLoader;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            SceneLoader.GetComponent<SceneLoader>().CutBackToMainScene();
        }
    }
}
