using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickpocketTarget : MonoBehaviour
{
    public GameObject hand;
    public float pickpocketStartDistance = 0.5f;
    private bool triggered = false;
    public SceneLoader SceneLoader;

    // Update is called once per frame
    void Update()
    {
        if ((Vector2.Distance(transform.position, hand.transform.position) < pickpocketStartDistance) && !triggered)
        {
            SceneLoader.CutToMiniGame();
            triggered = true;
        }

        if ((Vector2.Distance(transform.position, hand.transform.position) > 3) && triggered)
        {
            triggered = false;
        }

    }
}
