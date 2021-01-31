using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaurd : MonoBehaviour
{
    
    public float moveSpeed = 0.2f;
    public GameObject armObsticle;
    public GameObject hand;
    public float discoverRadius = 0.1f;
    public GameObject SceneLoader;
    private float timer;
    public int currentNode;
    private static Vector2 startPosition;
    public List<GameObject> nodes = new List<GameObject>(); 

    private void Start()
    {
        startPosition = nodes[1].transform.position;
        currentNode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime * moveSpeed;
        if (SceneLoader.GetComponent<SceneLoader>().InMainScene)
        {
            //if the gaurd is not close to the target
            if (Vector2.Distance(transform.position, nodes[currentNode].transform.position) > discoverRadius)
            {
                //lerp to it
                this.GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(transform.position, nodes[currentNode].transform.position, timer));
            }
            else
            {
                timer = 0;
                if (currentNode == nodes.Count - 1)
                {
                    nodes.Reverse();
                    currentNode = 0;
                }
                else
                {
                    currentNode++;
                }
            }
        }

        //sprite flipping
        if (transform.position.x < nodes[currentNode].transform.position.x)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
            

    }

    private void OnCollision2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        
    }
}
