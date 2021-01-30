using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickpocketTarget : MonoBehaviour
{
    public GameObject hand;
    public float pickpocketStartDistance = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, hand.transform.position) < pickpocketStartDistance)
        {
            Debug.Log("Pickpocket");
        }
    }
}
