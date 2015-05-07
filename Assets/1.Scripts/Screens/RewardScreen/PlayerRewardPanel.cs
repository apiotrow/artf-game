﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//controls reward panel for each player
public class PlayerRewardPanel : MonoBehaviour {
	public List<string> loot; //list of looted items
	public int total; //total points player has for voting
	public List<int> points; //points player has allocated to each item
	
	Transform lootList;
	RectTransform lootListRect;
	float newRowYPos;
	Vector2 iconDimens;
	Sprite icon;
	List<GameObject> highlights; //the highlighting on a selected item
	List<Text> pointsText; //the text for the points
	int activeEntry; //the entry currently selected
	RectTransform scrollView; //the container for the scroll field
	Text totalText;
	KeyCode up;
	KeyCode down;
	KeyCode add;
	KeyCode subtract;

	//joystick/button input setup
	public void setUpInputs(string upString, string downString, string addString, string subtractString){
		up = (KeyCode)System.Enum.Parse(typeof(KeyCode), upString);
		down = (KeyCode)System.Enum.Parse(typeof(KeyCode), downString);
		add = (KeyCode)System.Enum.Parse(typeof(KeyCode), addString);
		subtract = (KeyCode)System.Enum.Parse(typeof(KeyCode), subtractString);
	}

	void Start () {
		lootList = transform.Find("LootScroller/ScrollView/LootList");
		lootListRect = lootList.GetComponent<RectTransform>();
		loot = new List<string>();
		highlights = new List<GameObject>();
		points = new List<int>();
		pointsText = new List<Text>();
		totalText = transform.Find("Title/Text").GetComponent<Text>() as Text;

		//HARD-CODED SECTION
		//REPLACE WITH THINGS FROM OTHER SCRIPTS
		loot.Add("testIcon1");
		loot.Add("testIcon2");
		loot.Add("testIcon3");
		loot.Add("testIcon4");
		loot.Add("testIcon5");
		loot.Add("testIcon6");
		loot.Add("testIcon7");
		loot.Add("testIcon8");
		total = 13;
		//END HARDCODED SECTION

		populateList();

		highlights[0].SetActive(true); //initialize top entry to be highlighted
		activeEntry = 0;

		for(int i = 0; i < points.Count; i++){
			pointsText[i].text = "0";
		}
	}

	//populate list with looted items
	void populateList(){
		//spawn an icon just to get dimensions, then destroy it
		GameObject newLootItem = Instantiate(Resources.Load("RewardScreen/LootEntry")) as GameObject;
		iconDimens = newLootItem.GetComponent<RectTransform>().sizeDelta;
		Destroy (newLootItem);

		//create y-position of top entry
		newRowYPos = 0f - iconDimens.y / 2 - 5f;
		for(int i = 0; i < loot.Count; i++){
			makeNewEntry(loot[i]);
			highlights[i].SetActive(false);
		}
	}

	void Update(){
		takeInputs();
		updateHighlightedEntry();
		updateTexts();
	}

	void takeInputs(){

		//moves selector up and down list
		if(Input.GetKeyDown(down)){
			if(activeEntry < highlights.Count - 1)
				activeEntry += 1;
		}else if(Input.GetKeyDown(up)){
			if(activeEntry > 0)
				activeEntry -= 1;
		}

		//adds or subtracts points from an item
		if(Input.GetKeyDown(add)){
			if(total > 0){
				points[activeEntry] += 1;
				total -= 1;
			}
		}else if(Input.GetKeyDown(subtract)){
			if(points[activeEntry] > 0){
				total += 1;
				points[activeEntry] -= 1;
			}
		}
	}

	void updateTexts(){
		pointsText[activeEntry].text = points[activeEntry].ToString();
		totalText.text = total.ToString();
	}

	void updateHighlightedEntry(){
		for(int i = 0; i < highlights.Count; i++){
			if(i != activeEntry){
				highlights[i].SetActive(false);
			}else{
				highlights[i].SetActive(true);
			}
		}
	}
	
	//itemName is name of item
	void makeNewEntry(string itemName){
		GameObject newLootItem = Instantiate(Resources.Load("RewardScreen/LootEntry")) as GameObject;
		RectTransform lootItemRect = newLootItem.GetComponent<RectTransform>();

		//grow loot list rect to accommodate more entries
		lootListRect.sizeDelta = new Vector2(lootListRect.sizeDelta.x, lootListRect.sizeDelta.y + iconDimens.y);

		//parent entry to loot list scroll pane
		newLootItem.transform.SetParent(lootList, false);

		//set entry's position
		lootItemRect.anchoredPosition = new Vector2(0f + iconDimens.x / 2, newRowYPos);

		//give it the proper icon
		icon = Resources.Load<Sprite>("RewardScreen/" + itemName);
		newLootItem.transform.Find("Icon").GetComponent<Image>().sprite = icon;

		//add entry's height to list for highlighting
		highlights.Add(newLootItem.transform.Find("Selector").gameObject);

		//add points text to list for them
		pointsText.Add(newLootItem.transform.Find("Points/Text").gameObject.GetComponent<Text>() as Text);
		points.Add(0);

		//increment y position for next iteration
		newRowYPos -= iconDimens.y;
	}
	
}