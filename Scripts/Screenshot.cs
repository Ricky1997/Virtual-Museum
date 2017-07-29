/*
• SCREENSHOT MODULE
• 3/4/2017
• HarShit Bansal
• Synopsis - Takes Snapshot of the screen [without the GUI]

• Public Functions 
	TakeSS()
• Global variables accessed/modified by the module.
	canvasInterface
*/

using UnityEngine;
using System.Collections;
using System.IO;

public class Screenshot : MonoBehaviour {
// Main Canvas [Contains GUI]
	public Canvas canvasInterface;

// Click Event - Snapshot Button
	public void click()
	{
		StartCoroutine (TakeSS());
	}
// Take Snapshot
	public IEnumerator TakeSS()
	{
		yield return null;
	// Disable Canvas Before Taking Snapshot
		canvasInterface.enabled = false;
		yield return new WaitForEndOfFrame();
	// Capture Screenshot 
		Application.CaptureScreenshot ("ScreenShot" +Time.frameCount+ ".png");
	// Enable Canvas After Taking Snapshot
		canvasInterface.enabled = true;
	}
}
// ******************************* Class Definition Ends ****************************************//