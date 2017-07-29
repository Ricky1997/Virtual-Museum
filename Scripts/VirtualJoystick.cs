/*
• JOYSTICK MODULE
• 3/4/2017
• Author - Mayank Yadav
• Synopsis - Virtual Joystick Functions - Moving the Joystick only if the touch started on Joystick Background Image.
										- Joystick Image cannot go completely outside the Joystick Background Image
										- Player Moves based on difference between Joystick Image's current and original position
• Public Functions 
	 OnFingerDown()
	 OnFingerSet()
	 OnFingerUp()
	 Horizontal()
	 Vertical()
• Include Definition for Exhibit Class
• Global variables accessed/modified by the module.
	 layerMask 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Lean.Touch;


namespace Lean.Touch{
	public class VirtualJoystick : MonoBehaviour {
	// Public Variables - Accessed by the module from outside the Module
		public LayerMask layerMask = 1 << 8;
	
	// Private Variables
		private Image bgImg;
		private Image joystickImg;
		private Vector3 InputVector = Vector3.zero;
		private Vector2 pos;
		private bool startedOverJoystick = false;

		protected virtual void OnEnable()
		{
		// Hook into the events we need
			LeanTouch.OnFingerDown  += OnFingerDown;
			LeanTouch.OnFingerSet   += OnFingerSet;
			LeanTouch.OnFingerUp    += OnFingerUp;
		}
		protected virtual void OnDisable()
		{
		// Unhook the events
			LeanTouch.OnFingerDown  -= OnFingerDown;
			LeanTouch.OnFingerSet   -= OnFingerSet;
			LeanTouch.OnFingerUp    -= OnFingerUp;
		}
	// Get Image Components for Joystick and Joystick Background
		private void Start(){
			bgImg = GetComponent<Image> ();
			joystickImg = transform.GetChild (0).GetComponent<Image> ();
		}
	// When Starts the touch, Check if it started on the Joystick Background. If yes the Move Joystick
		private void OnFingerDown(LeanFinger finger)
		{
			if (finger.StartedOverGui == true) 
			{
				RaycastResult guiObj;
				guiObj = LeanTouch.RaycastGui (finger.ScreenPosition);
				if (guiObj.gameObject.CompareTag ("Joystick")) {
					startedOverJoystick = true;
					MoveJoystick (finger);
				}
			}
					
		}
	// If Touch [Finger] moves on screen joystick also moves IF the touch started from Joystick Background
		private void OnFingerSet(LeanFinger finger)
		{
			if (startedOverJoystick == true) {
				if (LeanTouch.PointOverLayer (finger.ScreenPosition, layerMask))
					MoveJoystick (finger);
			}

		}
	// Touch Ends - Reset Joystick Position to centre
		private void OnFingerUp(LeanFinger finger)
		{
			startedOverJoystick = false;
			InputVector = Vector3.zero;
			joystickImg.rectTransform.anchoredPosition = Vector3.zero;
		}
	// Move Joystick along with touch [Bounded by Joystick Background] - AND Assign Input Vector
		private void MoveJoystick(LeanFinger finger)
		{
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle (bgImg.rectTransform, finger.ScreenPosition, null, out pos)) 
			{
				pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
				pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

				InputVector = new Vector3 (pos.x * 2 + 1, 0, pos.y * 2 - 1);
				InputVector = (InputVector.magnitude > 1.0f) ? InputVector.normalized : InputVector;

				joystickImg.rectTransform.anchoredPosition = new Vector2 (InputVector.x * (bgImg.rectTransform.sizeDelta.x / 2), InputVector.z * (bgImg.rectTransform.sizeDelta.y / 2));
			}

		}
	// Return Horizontal Input Value
		public float Horizontal(){
			if(InputVector.x != 0)
				return InputVector.x;
			else
				return Input.GetAxis ("Horizontal");

		}
	// Return Vertical Input Value
		public float Vertical(){
			if (InputVector.z != 0)
				return InputVector.z;
			else
				return Input.GetAxis ("Vertical");	
		}
	}
}
// **************** Class Definition Ends ***************************************************//