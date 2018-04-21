using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum PlayerState { Waiting, Interacting }

public class Player : MonoBehaviour
{
    private const float INTERACT_DISTANCE = 2;

    private PlayerState state;

    private Food heldItem;

    public Food HeldItem { get { return heldItem; } set { heldItem = value; } }



    #region movement
    Rigidbody rigidbody;



    private Vector3 moveStart;
    private Vector3 moveEnd;
    private float lerpTime;
    #endregion

    // Use this for initialization
    void Start ()
    {
        state = PlayerState.Waiting;
        rigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //checks to see if any appliances are within range of the player
            List<Appliance> appliances = (from gameObject in GameObject.FindGameObjectsWithTag("Appliance") 
                where Vector3.Distance(gameObject.transform.position, transform.position) <= INTERACT_DISTANCE select gameObject.GetComponent<Appliance>()).ToList();
            
            //if they are, start the interaction
            if(appliances.Count > 0)
            {
                Appliance appliance = appliances[0];
                appliance.Interact(this);
            }
        }

        if (state == PlayerState.Waiting)
        {
            Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            movement.Normalize();
            movement *= 5;

            rigidbody.velocity = movement;




            ////create vector for movement
            //Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            //movement.Normalize();
            //moveEnd = transform.position + movement;

            //moveStart = transform.position;
            //lerpTime = 0f;

            ////set player to moving so as not to accept new input
            //if (movement != Vector3.zero)
            //{
            //    state = PlayerState.Moving;
            //}
        }

        if((Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) || state == PlayerState.Interacting)
        {
            
        }

        //if (state == PlayerState.Moving)
        //{
        //    //smoothly move player across distance
        //    lerpTime += Time.deltaTime * 5;
        //    transform.position = Vector3.Lerp(moveStart, moveEnd, lerpTime);

        //    //return player to resting state
        //    if(lerpTime >= 1f)
        //    {
        //        transform.position = moveEnd;
        //        state = PlayerState.Waiting;
        //    }
        //}
    }
}
