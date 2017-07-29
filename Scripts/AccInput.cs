/*
• ACCELERATION INPUT MODULE.
• 3/4/2017
• Mayank Yadav
• Synopsis - Gets the Accelerometer Values and processes them as Input for Camera Rotation and Camera Zoom

• Public Functions 
	Horizontal() - Returns Acceleration Value along X - Axis
	Vertical() - Returns Acceleration Value along Z - Axis
	SetBaseAcc() - Change Value of Base Acceleration
	ResetBaseAcc() - Reset Value of Base Acceleration to Default
• Global variables accessed/modified by the module.
	zoomScript - CameraZoomScript 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class AccInput : MonoBehaviour {
	
//**************************************************************************************************//
	// Class Variable Declarations

// Script handling the Pinch Zoom functionality
	public CameraZoomSmooth zoomScript;

// Time taken to adjust the Camera along -
	public float minVZoomLPKWidthInSecX = 0.5f;		// VERTICAL axis when Zoom is MINIMUM
	public float maxVZoomLPKWidthInSecX = 2.5f;		// VERTICAL axis when Zoom is MAXIMUM	
	public float minHZoomLPKWidthInSecX = 0.0f;		// HORIZONTAL axis when Zoom is MINIMUM
	public float maxHZoomLPKWidthInSecX = 1.0f;		// HORIZONTAL axis when Zoom is MAXIMUM

// Base Acceleration Values of the Device i.e Default ORIENTATION of the Device
	private Vector3 baseAcceleration = Vector3.zero;

// Live Input Values From the ACCELEROMETER - Current Orientation of the device
	private Vector3 currentAcc;									// Current Acceleration Vector (Along All Axis - X,Y,Z)								
	private Vector3 currentAccVerticalOnly = Vector3.zero; 		// Vertical Component Only ( Along Z Axis )
	private Vector3 currentAccHorizontalOnly = Vector3.zero;	// Horizontal Component Only ( Along X Axis )

// Time interval between UPDATION of Acceleration Values 
	private float accUpdateIntervalX = 1.0f / 60.0f;

// Linearly Interpolated value of Acceleration - Using Lerp Function
	private Vector3 lowPassValueV = Vector3.zero; 	// Vertical
	private Vector3 lowPassValueH = Vector3.zero;	// Horizontal

// Final Acceleration Values 
	private Vector3 finalAcc = Vector3.zero;

// Lowpass Factors - Factor that decides the rate of linear Interpolation between Previous Value and Current Value
	private float maxVZoomLPFFactor;    	// For Maximum Zoom - Vertical
	private float minVZoomLPFFactor;		// For Minimum Zoom - Vertical
	private float maxHZoomLPFFactor;		// For Maximum Zoom - Horizontal
	private float minHZoomLPFFactor;		// For Minimum Zoom - Horizontal

// Current Lowpass Factor 
	private float currentVZoomLPFFactor;	// Vertical
	private float currentHZoomLPFFactor;	// Horizontal

//***************************************************************************************************//
	// Class Function Definitions

// Start is called only once ** UNITY FUNCTION
// Calculate Values For the Lowpass Factors for  - 
	void Start () {
		minVZoomLPFFactor = accUpdateIntervalX / minVZoomLPKWidthInSecX;	// Minimum Zoom - Vertical
		maxVZoomLPFFactor = accUpdateIntervalX / maxVZoomLPKWidthInSecX;	// Maximum Zoom - Vertical
		minHZoomLPFFactor = accUpdateIntervalX / minHZoomLPKWidthInSecX;	// Minimum Zoom - Horizontal
		maxHZoomLPFFactor = accUpdateIntervalX / maxHZoomLPKWidthInSecX;	// Maximum Zoom - Horizontal
	}
	
// Update is called once per frame ** UNITY FUNCTION
// Calculate The Final Acceleration Values from the Input Acceleration Values each Frame
	void Update () {

	// Current Lowpass Factor is Scaled between it's Maximum and Mininum Values
		currentVZoomLPFFactor = ScaleValue(minVZoomLPFFactor,maxVZoomLPFFactor,zoomScript.Maximum,zoomScript.Minimum,zoomScript.GetCurrent());
		currentHZoomLPFFactor = ScaleValue(minHZoomLPFFactor,maxHZoomLPFFactor,zoomScript.Maximum,zoomScript.Minimum,zoomScript.GetCurrent());
	
	// If not set, Base Acceleration is zero initially . So Current Acceleration = Input Acceleration
		currentAcc = Input.acceleration - baseAcceleration;
		currentAccVerticalOnly.z = currentAcc.z;
		currentAccHorizontalOnly.x = currentAcc.x;
	
	// Lerp - Linear Interpolation  
		lowPassValueV = Vector3.Lerp(lowPassValueV,currentAccVerticalOnly,currentVZoomLPFFactor);	// Vertical
		lowPassValueH = Vector3.Lerp(lowPassValueH,currentAccHorizontalOnly,currentHZoomLPFFactor);	// Horizontal
	
	// Final Accleration Values - After Interpolation
		finalAcc.z = -lowPassValueV.z;				// Values Along Z-axis Are inverted - For Orientaion Purposes
		finalAcc.x = lowPassValueH.x;

	}

// Scale Conversion
// Scale a valueA (between minA and maxB) is SCALED to valueB (between minB and maxB)
	private float ScaleValue(float fromV1,float toV1,float fromV2, float toV2, float value){
		if (value <= fromV2)
			return fromV1;
		else if (value >= toV2)
			return toV1;
		else
			return (toV1 - fromV1) * ((value - fromV2) / (toV2 - fromV2)) + fromV1;
	}

// Return The Horizontal Component of Final Acceleration
	public float Horizontal(){
		return finalAcc.x;
	}

// Return The Horizontal Component of Final Acceleration
	public float Vertical(){
		return finalAcc.z;
	}

// Set Current Acceleration ( i.e Current Device Orientation) as 
// Base Acceleration (i.e DEFAULT Device Orientation)
	public void SetBaseAcc(){
		baseAcceleration = Input.acceleration;
	}

// RESET Current Acceleration ( i.e Current Device Orientation)
	public void ResetBaseAcc(){
		baseAcceleration = Vector3.zero;
	}
}
// ***** Class Definition Ends **************************************************************//