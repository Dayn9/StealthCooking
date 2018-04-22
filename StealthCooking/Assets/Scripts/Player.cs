using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum PlayerState { Waiting, Interacting }

public class Player : MonoBehaviour
{
    private const float INTERACT_DISTANCE = 2;

    Rigidbody rigidbody;

    private PlayerState state;

    private Food heldItem;

    /// <summary>
    /// Gets or sets the item that the player is holding
    /// </summary>
    public Food HeldItem { get { return heldItem; } set { heldItem = value; } }
    /// <summary>
    /// Gets or sets the state of the player
    /// </summary>
    public PlayerState State { get { return state; } set { state = value; if (state == PlayerState.Interacting) { rigidbody.velocity = Vector3.zero; } } }




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
        }

        if((Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) || state == PlayerState.Interacting)
        {
            
        }
    }
}
