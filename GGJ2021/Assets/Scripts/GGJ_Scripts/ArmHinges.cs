using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmHinges : MonoBehaviour
{
    private HingeJoint2D hinge;
    private void Start()
    {
        hinge = this.GetComponent<HingeJoint2D>();
    }

    private void Update()
    {
        

    }
}
