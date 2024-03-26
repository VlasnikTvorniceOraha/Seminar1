using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldObjects : MonoBehaviour
{
    //klasa za drzanje objekta pred sobom
    
    public GameObject heldObject;

    public GameObject parent; //roditelj za objekt da se rotira kak spada

    public GameObject prijasnjiParent; //roditelj kojem ce se vratiti

    public KeyCode pickUpKey;

    public KeyCode rotationKey;

    LayerMask itemLayer;

    public bool holding;

    bool hasGravity;

    Rigidbody objectBody;


    void Start()
    {
        itemLayer = LayerMask.GetMask("Item");
        holding = false;
    }

    // Update is called once per frame
    void Update()
    {

        //raycast koji udara neki item

        RaycastHit hit;

        if (holding && Input.GetKeyDown(pickUpKey)) {
            //pusti item, provjeri je li unutar neke sobe
            
            heldObject.transform.SetParent(prijasnjiParent.transform, true);
            if (hasGravity) {
                objectBody.useGravity = true;
            }
            
            objectBody.constraints = RigidbodyConstraints.None;
            heldObject = null;
            holding = false;
            Debug.Log("Pustoje");
        } else if (!holding && Input.GetKeyDown(pickUpKey)) {
            //raycastaj za neki item
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 4, Color.red, 4);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4, itemLayer)) {
                //pokupi item
                heldObject = hit.transform.gameObject;
                holding = true;
                prijasnjiParent = heldObject.transform.parent.gameObject;
                heldObject.transform.SetParent(parent.transform, true);
                Debug.Log("Drzoje");
                //iskljuci gravitaciju dok drzis
                objectBody = heldObject.GetComponent<Rigidbody>();
                hasGravity = objectBody.useGravity;
                if (hasGravity) {
                    objectBody.useGravity = false;
                }
                
                objectBody.angularVelocity = new Vector3(0f, 0f, 0f);
                objectBody.velocity = new Vector3(0f, 0f, 0f);
                objectBody.constraints = RigidbodyConstraints.FreezeRotation;
               
            }

        } else if (holding) {
            //holding something, reset velocity
            objectBody.velocity = new Vector3(0f, 0f, 0f);
        }

        //ako imamo item, R key za popravljanje rotacije

        if (holding && Input.GetKeyDown(rotationKey)) {

            heldObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            heldObject.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, 2f);

        }

        if (holding && Input.mouseScrollDelta.magnitude != 0f) {
            //pomakni objekt prema igracu ili dalje od igraca ovisno o trenutnoj udaljenosti

            float distance = Vector3.Distance(heldObject.transform.position, Camera.main.transform.position);
            //Debug.Log(distance);
            if (distance > 1.5f && Input.mouseScrollDelta.y < 0) {
                heldObject.transform.localPosition += new Vector3(0f, 0f, Input.mouseScrollDelta.y / 5);
            } else if (distance < 3.0f && Input.mouseScrollDelta.y > 0) {
                heldObject.transform.localPosition += new Vector3(0f, 0f, Input.mouseScrollDelta.y / 5);
            }
        }




        
    }
}
