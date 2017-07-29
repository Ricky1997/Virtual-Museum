/*
• PAUSE MODULE
• 3/4/2017
• Author - Mayank Yadav
• Synopsis - Pause the Tour - Time is frozen 

• Public Functions 
	Pause() - Sets Timescale = 0 i.e TIME STOPS !
• Global variables accessed/modified by the module.
	canvas - Pause Menu Gameobject
*/

using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	
// Pause Menu Gameobject
	public Transform canvas;

// Sets Timescale = 0  for pause , And Timescale = 1 for unpause
	public void Pause()
	{
	// If Pause Menu is NOT Active , Activate Pause Menu and Set Timescale to zero
		if (canvas.gameObject.activeInHierarchy == false)
		{
			canvas.gameObject.SetActive(true);
			Time.timeScale = 0;
		}
	// If Pause Menu is Active , De-activate Pause Menu and Set Timescale to 1
		else
		{
			canvas.gameObject.SetActive(false);
			Time.timeScale = 1;
		}
	}

}
// **************** Class Definition Ends **************************************//