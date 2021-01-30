using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject sceneLoader;
    public GameObject hand;
    public float camSpeed = 0.1f;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (sceneLoader.GetComponent<SceneLoader>().InMainScene)
        {
            Vector3 handPos = hand.transform.position;
            handPos.z -= 10;
            transform.position = Vector3.Lerp(transform.position, handPos, camSpeed);
        }
    }
}
