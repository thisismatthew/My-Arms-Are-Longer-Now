using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arm : MonoBehaviour
{
    private Vector2 currentPos;
    private Vector2 retractStartPos;
    private Vector2 nextRetractPos;
    private List<Vector2> armPositions = new List<Vector2>();
    private List<GameObject> armPieces = new List<GameObject>();
    private float timer;
    private Vector3 mousePosition;


    public GameObject debt;
    public bool hasMoney = false;
    public GameObject SceneLoader;
    public float moreArmDistance = 0.1f;
    public GameObject armPiece;
    public float snapbackAccuracyDist = 0.5f;
    public float snapbackSpeed = 0.1f;
    public float moveSpeed = 0.1f;
    public Sprite reaching_hand;
    public Sprite returning_hand;

    private bool stretching = false;
    private bool retracting = false;
    private bool forceRetract = false;




    private void Start()
    {
        currentPos = this.transform.position;
        armPositions.Add(currentPos);
        nextRetractPos = currentPos;
    }

    void FixedUpdate()
    {
        // Metaball arm pieces cant be above 450 or the renderer will give up 
        // check to make sure they are under the limit, if they are, retract. 
        if(armPieces.Count >= 450)
        {
            if (!retracting)
            {
                FindObjectOfType<AudioManager>().Play("retract");
                retracting = true;
                stretching = false;
                forceRetract = true;
            }
            retractStartPos = transform.position;
            Retract();
        }

        HandMatch();
        timer = Time.deltaTime * snapbackSpeed;
        //rather than bopping between scenes were just going to keep it all loaded and move the camera. 
        if (SceneLoader.GetComponent<SceneLoader>().InMainScene)
        {
            if (!hasMoney)
            {
                GetComponentInChildren<SpriteRenderer>().sprite = reaching_hand;
                if (Input.GetMouseButton(0) && !forceRetract)
                {
                    if (!stretching)
                    {
                        FindObjectOfType<AudioManager>().Play("stretch");
                        stretching = true;
                    }
                    Stretch();
                }
                else
                {
                    if (!retracting)
                    {
                        FindObjectOfType<AudioManager>().Play("retract");
                        retracting = true;
                        stretching = false;
                    }
                    retractStartPos = transform.position;
                    Retract();
                }
            }

            if (hasMoney)
            {
                GetComponentInChildren<SpriteRenderer>().sprite = returning_hand;
                retractStartPos = transform.position;
                Retract();
            }

        }

        //sprite flipping
        if (transform.position.x < mousePosition.x)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
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

            if(hasMoney && armPositions.Count == 1)
            {
                GetComponentInChildren<SpriteRenderer>().sprite = reaching_hand;
                hasMoney = false;
                //reduce debt amount by $10
                debt.GetComponent<Debt>().DebtValue -= 10;
            }

            if (armPositions.Count == 1 && retracting)
            {
                forceRetract = false;
                retracting = false;
                FindObjectOfType<AudioManager>().Stop("stretch");
                FindObjectOfType<AudioManager>().Stop("retract");

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

    private void HandMatch()
    {
        if (armPieces.Count > 0)
        {
            //get Hand sprite
            GameObject sprite = this.transform.GetChild(0).gameObject;
            //get last armpiece sprite
            GameObject lastArmSprite = armPieces[armPieces.Count - 1].transform.GetChild(0).gameObject;

            Vector3 spritePos = sprite.transform.localPosition;
            Vector3 lastArmSpritePos = lastArmSprite.transform.localPosition;

            //set them to be the same on the y axis. 
            spritePos.y = lastArmSpritePos.y;
            sprite.transform.localPosition = spritePos;
        }
    }
}
