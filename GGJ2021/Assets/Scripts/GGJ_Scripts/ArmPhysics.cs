using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPhysics : MonoBehaviour
{
    private List<GameObject> armPieces;
    private GameObject hand;
    private float time = 0f;
    private int index = 0;
    public ArmPhysics(List<GameObject> _armPieces, GameObject _hand)
    {
        armPieces = _armPieces;
        hand = _hand;
    }

    // create a wave moving down the arm based on the movements of the hand. 
    // as long as we alter the y position of the sprite (child) rather than the y position of the parent gameobject,
    // the rotation of the arm piece should not make the wave inconsistent. 

    private void Update()
    {
        time += Time.deltaTime;
        TestWiggle();
    }

    private void TestWiggle()
    {
        GameObject sprite = armPieces[index].transform.GetChild(0).gameObject;
        Vector3 spritePos = sprite.transform.localPosition;
        spritePos.y += Mathf.Sin(time);
        sprite.transform.localPosition = spritePos;

        if (index == armPieces.Count - 1) {
            index = 0;
        }
        else
        {
            index++;
        }
        
    }
}
