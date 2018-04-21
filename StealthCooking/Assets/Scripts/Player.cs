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
		if(state == PlayerState.Waiting)
        {
            
        }
	}
}
