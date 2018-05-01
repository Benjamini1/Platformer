using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	private float inputDirection;			// Xvalue of move
	private float verticalVelocity;	
	//Yvalue of move

	private Vector3 moveVector; 
	private CharacterController controller;

	public float gravity = 1.0f;
	public float speed = 5.0f;
	private bool secondJumpAvail = false;
	private Vector3 lastMotion;
	public float jumpForce = 20.0f;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		moveVector = Vector3.zero;
		inputDirection = Input.GetAxis("Horizontal") * speed;
	
		if (controller.isGrounded) 
		{
			verticalVelocity = 0;

			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				//make player jump
				verticalVelocity = jumpForce;
				secondJumpAvail = true;
			}
			moveVector.x = inputDirection;
		}
		else 
		{
			verticalVelocity -= gravity;

			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				if (secondJumpAvail) 
				{
					verticalVelocity = jumpForce;
					secondJumpAvail = false;
				}
			}

			verticalVelocity -= gravity * Time.deltaTime;
			moveVector.x = lastMotion.x;
		}

		moveVector.y = verticalVelocity;

		controller.Move (moveVector * Time.deltaTime);
		lastMotion = moveVector;
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (controller.collisionFlags == CollisionFlags.Sides)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				Debug.DrawRay (hit.point, hit.normal,Color.red, 2.0f);
				moveVector = hit.normal * speed;
				moveVector.y = jumpForce;
				secondJumpAvail = true;
			}		
		}
	}
}