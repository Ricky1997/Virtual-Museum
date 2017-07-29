/*
• LIST CONTROLLER
• 1/4/2017
• Author - HarShit Bansal
• Synopsis - Manage Scene Loading

• Public Functions 
	LoadLevel()

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class MenuManager : MonoBehaviour {

// Load new Scene
	public void LoadLevel(int level){
		SceneManager.LoadScene (level,LoadSceneMode.Single);
	}
}
