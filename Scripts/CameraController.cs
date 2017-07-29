/*
• CAMERA CONTROLLER.
• 4/4/2017
• Author - Nikunj Mittal
• Synopsis - Rotates Camera According to Acceleration Input
             Translates Camera According to Player Movement
             
• Global variables accessed/modified by the module.
	player - Player Gameobject
	accCam - Acceleration Input Script 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

// Player Gameobject - To which the Camera is attached
	public GameObject player;
// Acceleration Input Script - Provides Values for Camera Along X - Y Axis
	public AccInput accCam;

// Vertical Camera Rotation is CLAMPED between a Minimum and Maximum
	public float vMin = -10.0f;
	public float vMax = 10.0f;

// Speed of Movement of Camera
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
	private Vector3 offset;

	void Start ()
	{
		offset = transform.position - player.transform.position;
	}
    void Update()
    {
// yaw - Rotation along the Horizontal
		yaw += speedH * accCam.Horizontal ();
// pitch - Rotation along the Vertical 
		pitch = speedV * accCam.Vertical();

// Clamp the Vertical Rotation Values - To Avoid full rotation along Vertical
		pitch = Mathf.Clamp(pitch,vMin,vMax);
// Assign the Values to the Camera
		transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
	void LateUpdate ()
	{
// Camera moves along with the player
		transform.position = player.transform.position + offset;
    }
}
// ******* Class Definition Ends *******************************************************//