using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCollideArm : MonoBehaviour
{
    private Collider2D thisCollider;
    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "armJoint")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), thisCollider);
        }
    }
}
