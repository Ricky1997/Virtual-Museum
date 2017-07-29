/*
• OBJECT VIEWER - OBJECT TRANSLATE
• 3/4/2017
• Mayank Yadav
• Synopsis - Move Object in Object Viewer in a bounded area

• Public Functions 

• Global variables accessed/modified by the module.
	IgnoreGuiFingers
	RequiredFingerCount
	screenScale
*/

using UnityEngine;

namespace Lean.Touch
{
// This script allows you to transform the current GameObject
	public class ObjectTranslate : MonoBehaviour
	{
	// Ignore fingers with StartedOverGui?"
		public bool IgnoreGuiFingers = false;

	// Ignore fingers if the finger count doesn't match? (0 = any)"
		public int RequiredFingerCount;

	// The camera the translation will be calculated using (default = MainCamera)"
		public float screenScale;
	
	// Size of the Object - max[width,height,length]
		private float objectSize = 10.0f;

		protected virtual void Update()
		{
		// Get the fingers we want to use
			var fingers = LeanTouch.GetFingers(IgnoreGuiFingers, RequiredFingerCount);

		// Calculate the screenDelta value based on these fingers
			var screenDelta = LeanGesture.GetScreenDelta(fingers);
		
		// Perform the translation
			Translate(screenDelta);
		}
	// Move The Object in View
		private void Translate(Vector2 screenDelta)
		{
			Vector3 newPosition = transform.localPosition;
			Vector2 tempDelta;
		// tempDelta = 2D Vector of Movement of Object
			tempDelta = screenDelta * ((screenScale * objectSize) / 100.0f);
			newPosition += (Vector3)tempDelta;
		// Object in View's position w.r.t parent Container object
			transform.localPosition = new Vector3(Mathf.Clamp(newPosition.x,-objectSize,objectSize),Mathf.Clamp(newPosition.y,-objectSize,objectSize),newPosition.z);

		}
	// Set Object Size - Used to decide the boundary of area of allowed movement of the object
		public void SetObjectSize(float size){
			objectSize = size;
		}
	}
}