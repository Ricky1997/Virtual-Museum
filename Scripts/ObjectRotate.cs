/*
• OBJECT VIEWER - OBJECT ROTATE.
• 3/4/2017
• Mayank Yadav
• Synopsis - Rotate the Object in Object Viewer

• Public Functions - None
• Global variables accessed/modified by the module.
	IgnoreGuiFingers
	RequiredFingerCount
	screenScale
	sandboxMode
*/

using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to transform the current GameObject
	public class ObjectRotate : MonoBehaviour
	{
	//"Ignore fingers with StartedOverGui?"
		public bool IgnoreGuiFingers = false;

	//"Ignore fingers if the finger count doesn't match? (0 = any)"
		public int RequiredFingerCount;

	//"The camera the translation will be calculated using (default = MainCamera)"
		public float screenScale;

	//"Sandbox Mode Allows Free rotation along all 3 Axis [ Normal Mode Allows only Horizontal Rotation ]
		public bool sandboxMode = false;

	// Update is Called Every Frame
		protected virtual void Update()
		{
			// Get the fingers we want to use
			var fingers = LeanTouch.GetFingers(IgnoreGuiFingers, RequiredFingerCount);

			// Calculate the screenDelta value based on these fingers
			var screenDelta = LeanGesture.GetScreenDelta(fingers);
			var degrees = LeanGesture.GetTwistDegrees(fingers);

			// Perform the translation
			Rotate(screenDelta,degrees);
		}
	// Performs Rotation on the Object 
		private void Rotate(Vector2 screenDelta,float degreeZ)
		{
		// Sandbox Mode - Allow Rotation Along All 3 Axis (X,Y,Z)
			if (sandboxMode == true) {
				float degreeX, degreeY;
			// Calculate Rotation Values from screenDelta [Screen Delta = Change in touch position on screen for a finger]
				degreeY = -1 * screenScale * screenDelta.x;
				degreeX = screenScale * screenDelta.y;
			// Rotate According to World Axis
				transform.Rotate (degreeX,degreeY,degreeZ,Space.World );
			} 
			else {
				float degreeY = -1 * screenScale * screenDelta.x;
			// Rotate According to World Axis
				transform.Rotate (0,degreeY,0,Space.World );
			}
		}
	}
}
// ******************* Class Definition Ends ************************************* //