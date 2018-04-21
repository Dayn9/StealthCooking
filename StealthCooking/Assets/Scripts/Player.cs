using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Waiting, Moving, Interacting }

public class Player : MonoBehaviour
{
    private PlayerState state;

    private Vector3 moveStart;
    private Vector3 moveEnd;
    private float lerpTime;

	// Use this for initialization
	void Start ()
    {
        state = PlayerState.Waiting;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //check for possible interactions
        }

        if (state == PlayerState.Waiting)
        {
            //create vector for movement
            Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            movement.Normalize();
            moveEnd = transform.position + movement;

            moveStart = transform.position;
            lerpTime = 0f;

            //set player to moving so as not to accept new input
            if (movement != Vector3.zero)
            {
                state = PlayerState.Moving;
            }
        }

        if (state == PlayerState.Moving)
        {
            //smoothly move player across distance
            lerpTime += Time.deltaTime * 5;
            transform.position = Vector3.Lerp(moveStart, moveEnd, lerpTime);

            //return player to resting state
            if(lerpTime >= 1f)
            {
                transform.position = moveEnd;
                state = PlayerState.Waiting;
            }
        }
    }
}
