using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaurd : MonoBehaviour
{
    public float wanderRadius = 0.5f;
    public float moveSpeed = 0.2f;
    public GameObject armObsticle;
    public float discoverRadius = 0.1f;
    private float timer;
    private Vector2 currentMoveTarget;
    private static Vector2 startPosition;

    private void Start()
    {
        startPosition = this.transform.position;
        currentMoveTarget = getRandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime * moveSpeed;
        //if the gaurd is not close to the target
        if (Vector2.Distance(transform.position, currentMoveTarget) > discoverRadius)
        {
            //lerp to it
            this.GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(transform.position, currentMoveTarget, timer));
        }
        else
        {
            timer = 0;
            currentMoveTarget = getRandomPoint();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("You're in trouble");
    }

    private Vector2 getRandomPoint()
    {
        //this should mean that the gaurd does not wander outside the given radius centered on the start position;
        return startPosition + (Random.insideUnitCircle * wanderRadius);
    }
}
