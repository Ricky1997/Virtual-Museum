/*
• WISHLIST MODULE
• 3/4/2017
• Author - Mayank Yadav
• Synopsis - Manages All Wishlist and Catalogue Functions
• Public Functions 
	 AddToWishlist() 
	 LoadExhibit()
	 NextWishlistItem()
	 PrevWishlistItem()
	 ResetWishlistIndex()
	 ResetWishlist()
	 GetDescription()
	 MakeCatalogue()
	 WriteCatalogue()
• Include Definition for Exhibit Class
• Global variables accessed/modified by the module.
	 CatalogueController  
	 ViewObjectScript  
	 ListItemActive 
	 ListItemDisabled 
	 Exhibits 
	 Description  
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WishlistManager : MonoBehaviour {

// Public Variables - Accessed by the module from outside the Module
	public ListController CatalogueController;	// List Controller for Catalogue [List in GUI]
	public ViewObject ViewObjectScript;			// ViewObject Script
	public Sprite ListItemActive;				// Image/Icon for Item Added to Wishlist
	public Sprite ListItemDisabled;				// Image/Icon for Item Removed From Wishlist
	public Transform Exhibits;					// Parent GameObject Containing ALL Exhibits
	public Text Description;					// Description of Object

// Private Variables 
	private List<Exhibit> Catalogue = new List<Exhibit>();	// List of All Exhibits
	private List<Exhibit> Wishlist = new List<Exhibit> ();	// List of Exhibits User Wants to View
	private int wishlistIndex = 0;							// Index of Current Object in View in Wishlist
	private int catalogueCount = 0;							// Total no. of Exhibits

// When the Scene is Loaded - Extract List of Objects from 'Exhibits' , and Add them to Catalogue
	void Start(){
		MakeCatalogue ();
		WriteCatalogue ();
	}
// Add an Exhibit to the Wishlist
	public void AddToWishlist(ListItemController item){
		if (item.status == false) {
			item.gameObject.transform.GetChild (1).GetComponent<Image> ().sprite = ListItemActive;
			Catalogue [item.index].inWishlist = true;
			Wishlist.Add (Catalogue[item.index]);
			item.status = true;							// status = 1 => Present in Wishlist
		} 
		else {
			item.gameObject.transform.GetChild (1).GetComponent<Image> ().sprite = ListItemDisabled;
			Catalogue [item.index].inWishlist = false;
			Wishlist.Remove (Catalogue[item.index]);
			item.status = false;
		}
	}
// Load Exhibit at Current Wishlist Index to Object Viewer
	public void LoadExhibit(){
		
		if(Wishlist.Count>0){
			if (wishlistIndex >= Wishlist.Count) {
				wishlistIndex = Wishlist.Count - 1;
			}
		// Get ObjectViewer Ready - Destroy Exisiting objects, Get Current Object, Load it in Viewer
			ViewObjectScript.DestroyAllChildren ();
			ViewObjectScript.StartObjectViewer ();
			ViewObjectScript.GetObjectToBeViewed (Exhibits.GetChild (Wishlist [wishlistIndex].index).gameObject);
			ViewObjectScript.ViewSelectedObject ();
		}
	}
// Load Next Item in Wishlist
	public void NextWishlistItem(){
		if (wishlistIndex < Wishlist.Count - 1) {
			wishlistIndex++;
			LoadExhibit ();
		}
	}
// Load Previous Item in Wishlist
	public void PrevWishlistItem(){
		if (wishlistIndex > 0) {
			wishlistIndex--;
			LoadExhibit ();
		}
	}
// Reset WishlistIndex to 0
	public void ResetWishlistIndex(){
		wishlistIndex = 0;
	}
// Clear Wishlist
	public void ResetWishlist(){
		wishlistIndex = 0;
		Wishlist.Clear ();
	}

// Load Description of Current Object from it's corresponding file
	public void GetDescription(){
		Exhibit currExhibit = Wishlist [wishlistIndex];
		string filename = currExhibit.name;
		string contents;
		TextAsset txtAssets = (TextAsset)Resources.Load (filename);
		contents = txtAssets.text;
		Description.text = contents;
	}
// Get Objects from 'Exhibits', and store them into Catalogue
	private void MakeCatalogue(){
		int i;
		Transform obj;
		catalogueCount = Exhibits.childCount;
		for (i = 0; i < catalogueCount; i++) {
			obj = Exhibits.GetChild (i);
			Catalogue.Add(new Exhibit(obj.name,obj.GetComponent<Text>().text,false,i));
		}
	}
// Make a GUI List from Catalogue
	private void WriteCatalogue(){
		int i;
		for (i = 0; i < catalogueCount; i++) {
			CatalogueController.AddToList(Catalogue[i].name,Catalogue[i].shortDescription,false,i);
		}
	}
}
// **************** Class Definition Ends ***************************************************//

// Exhibit Class
public class Exhibit{
	public string name;
	public string shortDescription;
	public bool inWishlist;
	public int index;
// Constructor for Exhibit Class
	public Exhibit(string Name,string shortDesc,bool InWishlist,int Index){
		this.name = Name;
		this.inWishlist = InWishlist;
		this.shortDescription = shortDesc;
		this.index = Index;
	}
}
// **************** Class Definition Ends ***************************************************//