/*
• LIST CONTROLLER
• 1/4/2017
• Author - Mayank Yadav
• Synopsis - List Gameobject Functions [i.e Scrollable List in GUI]

• Public Functions 
	AddToList()
	DestroyAllChildren()
• Global variables accessed/modified by the module.
	ContentPanel - Panel Containing the List
	ListItemPrefab -  Template for List item
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListController : MonoBehaviour {

// Panel Containing the List
	public GameObject ContentPanel;
// Template for List item
	public GameObject ListItemPrefab;

// Add an Item to the List
	public void AddToList(string itemName,string shortDesc,bool status, int index){
		GameObject newItem = Instantiate(ListItemPrefab) as GameObject;
		ListItemController controller = newItem.GetComponent<ListItemController>();
		controller.itemName = itemName;
		controller.status = status;
		controller.index = index;

		newItem.SetActive (true);
		newItem.transform.GetChild (0).GetComponent<Text> ().text = itemName;
		newItem.transform.GetChild (2).GetComponent<Text> ().text = shortDesc;
		newItem.transform.SetParent(ContentPanel.transform,false);
	}

// Empty List - Destroy All List Item Gameobjects
	public void DestroyAllChildren(){
		Transform Content = ContentPanel.transform;
		int childs = Content.childCount;
		for (int i = childs-1; i >= 0; i--) {
			GameObject.Destroy (Content.GetChild (i).gameObject);
		}
	}

}
// **************** Class Definition Ends **************************************//