/*
• MiniMap Module
• 7/4/2017
• Author - HarShit Bansal
• Synopsis - Manage Mini Map

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

public class MapView : MonoBehaviour {

	public Transform player;
	public Image mapptr;
	public Transform viewmapbutton;
	public Transform viewmapwindow;
	public Transform input;
	public Transform inputobj;
	Vector3 temp;
	Vector3 pos;
	Vector3 dummy;
	void Start()
	{
		dummy = player.transform.position;
		pos = player.transform.position;
	}
	void Update()
	{
		pos = player.transform.position - dummy;
		Vector3 ptr = (5f * pos);
		mapptr.rectTransform.anchoredPosition = new Vector2 (ptr.x, ptr.z);
	}
	public void section()
	{
		temp =new Vector3(-4.5f,0f,4.5f);
		if (viewmapbutton.gameObject.activeInHierarchy == false){
			viewmapbutton.gameObject.SetActive(true);
		}
	}
	public void go()
	{
		player.transform.position = temp;
		viewmapbutton.gameObject.SetActive(false);
		back ();
	}
	public void disp()
	{
		//Time.timeScale = 0;
		viewmapwindow.gameObject.SetActive (true);
	}
	public void back()
	{
		//viewmapbutton.gameObject.SetActive(false);
		viewmapwindow.gameObject.SetActive (false);
		//Time.timeScale = 1;
	}
	public void dispnotes()
	{
		
		if (input.gameObject.activeInHierarchy == true) 
		{
			input.gameObject.SetActive(false);
		}
		else{input.gameObject.SetActive(true);}
			
	}
	public void hideobjectwindow()
	{

		if (inputobj.gameObject.activeInHierarchy == true) 
		{
			inputobj.gameObject.SetActive(false);
		}

	}





}
