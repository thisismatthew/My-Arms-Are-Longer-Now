using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    private Vector2 currentPos;
    private Vector2 nextRetractPos;
    private List<Vector2> armPositions = new List<Vector2>();
    private List<GameObject> armPieces = new List<GameObject>();
    private float timer;
    private Vector3 mousePosition;

    public float moreArmDistance = 0.1f;
    public GameObject armPiece;
    public float snapbackAccuracyDist = 0.5f;
    public float snapbackSpeed = 0.1f;
    public float moveSpeed = 0.1f;

    private void Start()
    {
        currentPos = this.transform.position;
        armPositions.Add(currentPos);
        nextRetractPos = currentPos;
    }

    void FixedUpdate()
    {
        timer = Time.deltaTime * snapbackSpeed;

        if (Input.GetMouseButton(0))
        {
            Stretch();
        }
        else
        {
            Retract();
        }
    }

    private void Stretch()
    {
        //lerp the hand to the mouse
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        this.GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(transform.position, mousePosition, moveSpeed));
        //point it towards the mouse while it does it. 
        RotateToFace(gameObject, mousePosition);
        //has the hand moved?
        if (ArmMoved())
        {
            CreateArmPiece();
        }
    }

    private void Retract()
    {
        //if we are done dragging the arm lets retract
        //check if the hand is close enough to the last arm piece
        if (Vector2.Distance(transform.position, nextRetractPos) > snapbackAccuracyDist)
        {
            //lerp to it if it has not
            this.GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(transform.position, nextRetractPos, timer));
            //point the hand to face away from the arm piece
            RotateToFace(gameObject, nextRetractPos);
            gameObject.transform.RotateAround(transform.position, transform.forward, 180f);
            //reset the current pos for the arm creation list. 
            currentPos = this.transform.position;
        }
        else
        {
            //if we have reached the last arm piece and there are pieces remaining...
            if (armPositions.Count > 1)
            {
                foreach (GameObject arm in armPieces)
                {
                    //delete the game object arm piece
                    Vector2 pos = arm.transform.position;
                    if (pos == armPositions[armPositions.Count - 1])
                    {
                        Destroy(arm);
                        armPieces.Remove(arm);
                        break;
                    }
                }
                //remove its coords, and set the next arm piece to retract to
                armPositions.RemoveAt(armPositions.Count - 1);
                timer = 0;
                nextRetractPos = armPositions[armPositions.Count - 1];
            }
        }
    }

    private void CreateArmPiece()
    {
        //create an object at the previous position. 
        GameObject newArmPiece = Instantiate(armPiece, armPositions[armPositions.Count - 1], Quaternion.identity);
        //rotate the armpiece so that it faces the hand
        RotateToFace(newArmPiece, gameObject);
        //add the object to the list of arm pieces
        armPieces.Add(newArmPiece);
        //add the current position to the list
        armPositions.Add(currentPos);
        //set the new current position
        currentPos = this.transform.position;
        nextRetractPos = armPositions[armPositions.Count - 1];
    }

    //object to object
    private void RotateToFace(GameObject a, GameObject b)
    {
        //rotates a to face b
        Vector3 vectorToTarget = a.transform.position - b.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        a.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); ;
    }

    //object to Vector3
    private void RotateToFace(GameObject a, Vector3 b)
    {
        //rotates a to face b
        Vector3 vectorToTarget = a.transform.position - b;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        a.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); ;
    }


    private bool ArmMoved()
    {
        //get the distance between the arms movement a tick ago and its current position
        if (Vector2.Distance(currentPos,this.transform.position) > moreArmDistance)
        {
            return true;
        }
        return false;
    }
}
