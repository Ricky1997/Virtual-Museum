/*

• Pinch Zoom Module
• 7/4/2017
• Author - Mayank Yadav
• Synopsis - Pinch Zoom - Zoom in/out is smoothe

*/

using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to zoom a camera in and out based on the pinch gesture
	// This supports both perspective and orthographic cameras
	public class CameraZoomSmooth : MonoBehaviour
	{
	//"Ignore fingers with StartedOverGui?"
		public bool IgnoreGuiFingers = true;

	//"Allows you to force rotation with a specific amount of fingers (0 = any)")
		public int RequiredFingerCount;

	//"If you want the mouse wheel to simulate pinching then set the strength of it here. Range(-1.0f, 1.0f)
		public float WheelSensitivity;

	//"The camera we will be moving")
		public Camera Camera;

	//"The target FOV/Size")
		public float Target = 10.0f;

	//"The minimum FOV/Size we want to zoom to")
		public float Minimum = 10.0f;

	//"The maximum FOV/Size we want to zoom to")
		public float Maximum = 60.0f;

	//"How quickly the zoom reaches the target value")
		public float Dampening = 10.0f;
	
	// Start - Runs at the start of Game - Once
		protected virtual void Start()
		{
			if (LeanTouch.GetCamera(ref Camera) == true)
			{
				Target = GetCurrent();
			}
		}
	// LateUpdate Runs after Every Frame
		protected virtual void LateUpdate()
		{
			// If camera is null, try and get the main camera, return true if a camera was found
			if (LeanTouch.GetCamera(ref Camera) == true)
			{
				// Get the fingers we want to use
				var fingers = LeanTouch.GetFingers(IgnoreGuiFingers, RequiredFingerCount);

				// Scale the current value based on the pinch ratio
				Target *= LeanGesture.GetPinchRatio(fingers, WheelSensitivity);

				// Clamp the current value to min/max values
				Target = Mathf.Clamp(Target, Minimum, Maximum);

				// The framerate independent damping factor
				var factor = 1.0f - Mathf.Exp(-Dampening * Time.deltaTime);

				// Store the current size/fov in a temp variable
				var current = GetCurrent();

				current = Mathf.Lerp(current, Target, factor);

				SetCurrent(current);
			}
		}
	// Get Current Camera Field of View
		public float GetCurrent()
		{
			if (Camera.orthographic == true)
			{
				return Camera.orthographicSize;
			}
			else
			{
				return Camera.fieldOfView;
			}
		}
	// Set Camera Field of View
		private void SetCurrent(float current)
		{
			if (Camera.orthographic == true)
			{
				Camera.orthographicSize = current;
			}
			else
			{
				Camera.fieldOfView = current;
			}
		}
	}
}