/*
• MODULE FOR SELECTING OBJECT
• 1/4/2017
• Mayank Yadav
• Synopsis - Uses Touch Input on screen to select object(exhibit) in world

• Public Functions 
	OnFingerTap()
• Global variables accessed/modified by the module.
	fpsCam 
	textObjectSelected
	layerMask
	viewObjectScript
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Lean.Touch;


namespace Lean.Touch{
	public class LolActualHandler : MonoBehaviour {

	// Name of the Selected Object
		public Text textObjectSelected;
	// Player Camera 
		public Camera fpsCam;
	// Layers to be used for Raycasting for object selection
		public LayerMask layerMask = Physics.DefaultRaycastLayers;
	// ViewObject Class Script
		public ViewObject viewObjectScript;
	// Selected Object
		private GameObject selectedObject;

	// Add Function OnFingerTap() as a listener to OnFingerTap Event
		protected virtual void OnEnable()
		{
			LeanTouch.OnFingerTap   += OnFingerTap;
		}
	// Remove Function OnFingerTap() as a listener to OnFingerTap Event
		protected virtual void OnDisable()
		{
			LeanTouch.OnFingerTap   -= OnFingerTap;
		}
	// Function OnFingerTap
		public void OnFingerTap(LeanFinger finger)
		{
			SelectObject (finger);
		}
	// Select the object in world space , according to touch input on the screen
		private void SelectObject(LeanFinger finger)
		{
		// Select Finger as input ONLY if the touch did not start over a GUI object
			if (!finger.StartedOverGui == true) 
			{
			// Ray that is cast from the point of touch on screen into the world
				Ray ray;
			// First Object the Raycast hits
				RaycastHit hit;

			// Cast Ray
				ray = fpsCam.ScreenPointToRay (finger.ScreenPosition);
			// If Ray hits any object specified on the layerMask
				if (Physics.Raycast (ray, out hit, Mathf.Infinity, layerMask)) 
				{
				// Check if the hit object is an Exhibit
					if (hit.collider.CompareTag ("Exhibit")) {
						selectedObject = hit.collider.gameObject;
						viewObjectScript.GetObjectToBeViewed (selectedObject);
						textObjectSelected.text = selectedObject.transform.name;
					} 
				// Else Find such an object in its Parent Recurisvely
					else {
						selectedObject = GetParentWithTag (hit.collider.gameObject, "Exhibit");
						if (selectedObject != null) {
						// Display Selected Object's Name
							textObjectSelected.text = selectedObject.transform.name;
						// Get the Object Ready for View
							viewObjectScript.GetObjectToBeViewed (selectedObject);
						// Get the Object's Description Ready in Object Viewer
							viewObjectScript.GetDescription (selectedObject);
						}
					}
				}
			}
		}
	// Return Parent Object with given Tag
		private GameObject GetParentWithTag(GameObject child,string tag){
			Transform t = child.transform;
		// Search Recursively in Parent for Gameobject with given tag
			while (t.parent != null) {
				if (t.parent.CompareTag (tag))
					return t.parent.gameObject;

				t = t.parent.transform;
					
			}
			return null;	
		}
	}
}
// ************************* Class Definition Ends ********************************* //