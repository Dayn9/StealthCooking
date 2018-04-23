using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum PlayerState { Waiting, Interacting }

public enum AnimationState { IdleRight, IdleLeft, IdleUp, IdleDown, WalkRight, WalkLeft, WalkUp, WalkDown,}

public class Player : MonoBehaviour
{
    [SerializeField] private Collider exit;

    private const float INTERACT_DISTANCE = 1.5f;

    Rigidbody rigidbody;

    private PlayerState state;

    private Food heldItem;

    private Animator animator;
    private AnimationState animState;

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

        animator = gameObject.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //interacting
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

        //movement
        if (state == PlayerState.Waiting)
        {
            Vector3 movement = Vector3.zero;
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            }
            movement.Normalize();
            movement *= 3;

            rigidbody.velocity = movement;

            if (movement.x > 0)
            {
                animState = AnimationState.WalkRight;
            }
            else if (movement.x < 0)
            {
                animState = AnimationState.WalkLeft;
            }
            else if (movement.z > 0)
            {
                animState = AnimationState.WalkUp;
            }
            else if (movement.z < 0)
            {
                animState = AnimationState.WalkDown;
            }
            else if (movement == Vector3.zero)
            {
                switch (animState)
                {
                    case AnimationState.WalkRight:
                        animState = AnimationState.IdleRight;
                        break;
                    case AnimationState.WalkLeft:
                        animState = AnimationState.IdleLeft;
                        break;
                    case AnimationState.WalkUp:
                        animState = AnimationState.IdleUp;
                        break;
                    case AnimationState.WalkDown:
                        animState = AnimationState.IdleDown;
                        break;
                }
            }
            
            //Animator.Play(string);
        }

        //animation
        switch (animState)
        {
            case AnimationState.WalkRight:
                animator.Play("WalkRight");
                SoundManager.AddSoundFootsteps(.4f, transform.position);
                break;
            case AnimationState.WalkLeft:
                animator.Play("WalkLeft");
                SoundManager.AddSoundFootsteps(.4f, transform.position);
                break;
            case AnimationState.WalkUp:
                animator.Play("WalkUp");
                SoundManager.AddSoundFootsteps(.4f, transform.position);
                break;
            case AnimationState.WalkDown:
                animator.Play("WalkDown");
                SoundManager.AddSoundFootsteps(.4f, transform.position);
                break;
            case AnimationState.IdleRight:
                animator.Play("IdleRight");
                break;
            case AnimationState.IdleLeft:
                animator.Play("IdleLeft");
                break;
            case AnimationState.IdleUp:
                animator.Play("IdleUp");
                break;
            case AnimationState.IdleDown:
                animator.Play("IdleDown");
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other == exit && RecipieManager.eaten)
        {
            Debug.Log("You win!");
        }
    }
}
