/*
• OBJECT VIEWER MODULE
• 3/4/2017
• Author - Mayank Yadav
• Synopsis - Manage all Object Viewer Functions
• Public Functions 
	 ViewSelectedObject()
	 GetObjectToBeViewed()
	 GetDescription()
	 SwitchMode()
	 enableSandbox()
	 disableSandbox()
	 DestroyAllChildren()
	 ResetObjectViewer()
	 StartObjectViewer()
	 ResetObject()
• Include Definition for Exhibit Class
• Global variables accessed/modified by the module.
	 Description
	 objectPostion
	 objectDistance
	 rotateScript
	 translateScript
	 zoomScript
	 sandboxD,sandboxE,toggleMode
	 resetPos 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ViewObject : MonoBehaviour {

// Public Variables - Accessed by the module from outside the Module [ i.e they are initializes in the UNITY Editer ]
	public Text Description;					
	public ObjectRotate rotateScript;
	public ObjectTranslate translateScript;
	public CameraZoomSmooth zoomScript;
	public Button sandboxD,sandboxE,toggleMode;
	public Vector3 resetPos;

// Private Variables

	private Vector3 objectPostion;
	private float objectDistance;
	private GameObject objectToBeViewed;
	private Transform objCollider;
	private Quaternion objectRotation;
	private Vector3 centerOffset = Vector3.zero;
	private int viewMode = 1;  // 1 -> Rotate, 0 -> Translate

// Instantiate Selected Object in the Object Viewer
	public void ViewSelectedObject(){
		if (objectToBeViewed == null)
			return;
		else {
		// Minimum Distance the Object should be from the Camera to fully inside the camera's field of view
			float viewDistance = objectDistance * 0.5f / Mathf.Tan (zoomScript.Maximum * 0.5f * Mathf.Deg2Rad);
			transform.position = resetPos + (Max(viewDistance,objectDistance,1.0f) * transform.forward) ;
			objectRotation = objectToBeViewed.transform.rotation;
			objectPostion = transform.position - centerOffset;
			Instantiate (objectToBeViewed,objectPostion,objectRotation,transform);
		// If Auto Tour , DeActivate the original reference Object
			if(SceneManager.GetActiveScene().buildIndex == 2)
				objectToBeViewed.SetActive (false);

		}
	}
// Gets selected Object and Stores Its 'Exhibit' Object[Complete Exhibit] and 'Collider' Object [Child with Collider] 
// Both May be Same
	public void GetObjectToBeViewed(GameObject objectReference){
		Transform temp;
		if (objectReference.CompareTag ("Exhibit"))
			objectToBeViewed = objectReference;
		else {
			temp = GetChildObjectWithTag (objectReference.transform, "Exhibit", true);
			if (temp != null)
				objectToBeViewed = temp.gameObject;
			else
				objectToBeViewed = objectReference;
		}
		objectToBeViewed.SetActive (true);
		if (objectToBeViewed.GetComponent<Collider> () == true) {
			objCollider = objectToBeViewed.transform;
		} 
		else {
			objCollider = GetChildObjectWithTag (objectToBeViewed.transform,"Collider"); 

		}
		Vector3 size = objCollider.GetComponent<Collider> ().bounds.size;
		Vector3 center = objCollider.GetComponent<Collider> ().bounds.center;
		centerOffset = center - objectToBeViewed.transform.position;
		objectDistance = Max (size.x,size.y,size.z);
		translateScript.SetObjectSize (objectDistance/2);

	}
// Get Description of Selected Object from File
	public void GetDescription(GameObject objectReference){
		string filename = objectReference.name;
		string contents;
		TextAsset txtAssets = (TextAsset)Resources.Load (filename);
		contents = txtAssets.text;

		Description.text = contents;
	}
// Toggle between Translate Mode and Rotate Mode
	public void SwitchMode(){
		if (viewMode == 1) {
			rotateScript.enabled = false;
			translateScript.enabled = true;
			viewMode = 2;
		} 
		else if (viewMode == 2) {
			rotateScript.enabled = true;
			translateScript.enabled = false;
			viewMode = 1;
		}
	}
// Switch to Sandbox Mode -> Rotation along All 3 Axis is allowed
	public void enableSandbox(){
		rotateScript.sandboxMode = true;
		rotateScript.enabled = true;
		translateScript.enabled = false;
		viewMode = 1;
	}
// Switch to Normal Mode -> Rotation along Horizontal Axis Only
	public void disableSandbox(){
		rotateScript.sandboxMode = false;
		rotateScript.enabled = true;
		translateScript.enabled = false;
		viewMode = 1;

	}
// Destroy All Objects in View [View -> in Object Viewer]
	public void DestroyAllChildren(){
		int childs = transform.childCount;
		for (int i = childs-1; i >= 0; i--) {
			GameObject.Destroy (transform.GetChild (i).gameObject);
		}

	}
// Reset Object Viewer
	public void ResetObjectViewer(){
		transform.position = new Vector3 (0,-80,20);
		transform.rotation = Quaternion.identity;
		rotateScript.enabled = false;
		translateScript.enabled = false;
	}
// Initialize Object Viewer
	public void StartObjectViewer(){
		rotateScript.enabled = true;
		translateScript.enabled = false;
		transform.position = new Vector3 (0,-80,20);
		transform.rotation = Quaternion.identity;
		sandboxE.gameObject.SetActive (true);
		sandboxD.gameObject.SetActive (false);
		disableSandbox ();
	}
// Reset Object's Position 
	public void ResetObject(){
		transform.position = objectPostion + centerOffset;
		transform.rotation = Quaternion.identity;
	}
// Auxilary Functions
// Find Child with given tag OR 'Exhibit' tag in children recursively
	private Transform GetChildObjectWithTag(Transform parent, string tag){
		for (int i = 0; i < parent.childCount; i++) {
			Transform child = parent.GetChild (i);
			Transform c2;
			if (child.CompareTag (tag) || child.CompareTag ("Exhibit")) {
				if (child.CompareTag ("Exhibit")) {
					if (child.GetComponent<Collider> () == true)
						return child;
				} 
				else 
					return child;
			}
			if (child.childCount > 0) {
				c2 = GetChildObjectWithTag (child, tag);
				if (c2 != null)
					return c2;
			}		
		}
		return null;
	}

	// Find Child with given tag ONLY in children recursively
	private Transform GetChildObjectWithTag(Transform parent, string tag, bool notforCollider){
		for (int i = 0; i < parent.childCount; i++) {
			Transform child = parent.GetChild (i);
			Transform c2;
			if (child.CompareTag (tag)) {
					return child;
				}
			if (child.childCount > 0) {
				c2 = GetChildObjectWithTag (child, tag);
				if (c2 != null)
					return c2;
			}		
		}
		return null;
	}
// Simply returns the Max value between A and B
	private float Max(float A,float B){
		if (A >= B)
			return A;
		else
			return B;
	}
// Simply returns the Max value between A, B and C
	private float Max(float A,float B, float C){
		return Max (Max(A,B),C);
	}

}
// **************** Class Definition Ends ***************************************************//