/*
• PLAYER CONTROLLER
• 5/4/2017
• Author - Nikunj Mittal
• Synopsis - Controls Player Movements , Jump Movement

• Public Functions 
	JumpInput()
• Global variables accessed/modified by the module.
	vjMov
	accCamc
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

// Public Variables - Accessed by the Module from outside
	public Lean.Touch.VirtualJoystick vjMov;	// Virtual Joystick - For Player Movement Input
	public AccInput accCam;						// Acceleration Input Script

// Public Variables - Can be Accessed by outside the Module
	public float jumpSpeed = 8.0f;				// Jump Speed -_-
	public float speed = 6.0f;					// Player Movement Speed
	public float gravity = 20.0f;				// Gravity Value
	public float speedH = 2.0f;					// Player Horizontal Rotation Speed 

// Private Variables 
	private bool jumpValue = false;					// Should I Jump? , says the Player
	private float yaw = 0.0f;						// All For Horizontal Rotation
	private Vector3 moveDirection = Vector3.zero;	// Direction in which Player is to Move
	private CharacterController controller;			// Character Controller Component of Player


	void Start()
	{
	// Get the Character Controller Component from Player GameObject
		controller = GetComponent<CharacterController> ();
	}
	// Update - Runs before every frame refresh - UNITY
	void Update()
	{
		// Rotate the Player as the Camera Rotates [Horizontal Only]
		yaw += speedH * accCam.Horizontal();
		transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
		// If Player is on Ground - Calculate Move Direction, Check for Jump Input
		if (controller.isGrounded) {
			moveDirection = new Vector3 (vjMov.Horizontal () , 0 , vjMov.Vertical ());
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if (JumpInput () == true) {
				JumpInput (false);
				moveDirection.y = jumpSpeed;
			}
		}
		// Effect of Gravity
		moveDirection.y -= gravity * Time.deltaTime;
		// Move Player in Calculated Direction
		controller.Move(moveDirection * Time.deltaTime);
	}
	// Check Status of Jump Input
	private bool JumpInput(){
		return jumpValue;
	}
	// Set Jump Input - Set to 1 for Jump
	public void JumpInput(bool newValue){
		jumpValue = newValue;
	}
}
// ***** Class Definition Ends **************************************************************//