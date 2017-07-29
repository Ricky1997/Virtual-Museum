/*
• MODULE FOR SELECTING SCREEN RESOLUTION
• 1/4/2017
• Author - Nikunj Mittal
• Synopsis - Select Resolution of device

• Public Functions 
	SetScreenRes()
	SetScreenRes(int)
• Global variables accessed/modified by the module.
	ResList - Dropdown Menu listing available resolutions
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolution : MonoBehaviour {

//Dropdown Menu listing available resolutions
	public Dropdown ResList;

// Initial Resolution - 1280 x 720
	void Start(){
		SetScreenRes (2);
	}
// Set Screen Resolution according to value selected in the Dropdown List
	public void SetScreenRes(){
	// Resolution index in Dropdown Menu
		int index = ResList.value;
		switch (index) {
		case 0:
			Screen.SetResolution (2560, 1440, true);
			break;
		case 1:
			Screen.SetResolution (1920, 1080, true);
			break;
		case 2:
			Screen.SetResolution (1280, 720, true);
			break;
		case 3:
			Screen.SetResolution (1136, 640, true);
			break;
		case 4:
			Screen.SetResolution (960, 540, true);
			break;
		case 5:
			Screen.SetResolution (800, 480, true);
			break;
		}
	}
// Select Resolution According to provided dropdown index
	public void SetScreenRes(int index){

		ResList.value = index;
		switch (index) {
		case 0:
			Screen.SetResolution (2560, 1440, true);
			break;
		case 1:
			Screen.SetResolution (1920, 1080, true);
			break;
		case 2:
			Screen.SetResolution (1280, 720, true);
			break;
		case 3:
			Screen.SetResolution (1136, 640, true);
			break;
		case 4:
			Screen.SetResolution (960, 540, true);
			break;
		case 5:
			Screen.SetResolution (800, 480, true);
			break;
		}
	}
}
// ******************* Class Definition Ends ****************************************** //