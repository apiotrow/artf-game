﻿using UnityEngine;
using System.Collections;
using System;
using System.IO; 
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ItemObject : MonoBehaviour
{
//	public LayerMask draggingLayerMask;
//	static Camera UICamera;
//	bool inMouseCheck = false;
//	Vector3 initMousePos;
//	TileMapController tilemapcont;
//	float mouseDeadZone = 10f;
//	Shader focusedShader;
//	Shader nonFocusedShader;
//	Vector3 newp;
//
//	void Start ()
//	{
//		UICamera = GameObject.Find ("UICamera").GetComponent<Camera>();
//		tilemapcont = GameObject.Find ("TileMap").GetComponent("TileMapController") as TileMapController;
//		
//		focusedShader = Shader.Find ("Transparent/Bumped Diffuse");
//		nonFocusedShader = Shader.Find ("Bumped Diffuse");
//		
//		this.gameObject.GetComponentInChildren<Renderer>().material.shader = nonFocusedShader;
//	}
//	
//	void Update ()
//	{
//		if (!Input.GetMouseButtonDown (0) || UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject () == true) 
//			return;
//		
//		Ray ray = UICamera.ScreenPointToRay (Input.mousePosition);
//		RaycastHit hit; 
//		
//		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
//			//check for tilemap so we don't try to drag it
//			if (hit.collider.gameObject.name != "TileMap" 
//			    && hit.collider.transform.root.gameObject.GetInstanceID () == this.gameObject.GetInstanceID ()) {
//				Debug.Log(hit.collider.transform.root.gameObject.name);
//				if (inMouseCheck == false) {
//					initMousePos = Input.mousePosition;
//					inMouseCheck = true;
//				}
////				ObjectFocus.focusedObject = this.gameObject;
//				StartCoroutine (DragObject (hit.distance));	
//			}
//		}
//	}
//
//	IEnumerator DragObject (float distance)
//	{
//		//for the ghost-duplicate
//		GameObject itemObjectCopy = null;
//		
//		bool cancellingMove = false;
//		bool outOfDeadZone = false;
//		//bool copyCreated = false;
//		newp = this.gameObject.transform.position;
//		
//		while (Input.GetMouseButton(0)) { 
//			
//			//if mouse left deadzone, and we haven't made a copy of the object yet
//			if (outOfDeadZone && itemObjectCopy == null) {
//				//create copy of item object
//				itemObjectCopy = Instantiate (this.gameObject, getPosition(), getRotation()) as GameObject;
//				//update the item object things
//				//shader has to be set in this loop, or transparency won't work
//				//itemObjectCopy.gameObject.GetComponentInChildren<Renderer>().material.shader = focusedShader;
//				foreach (Renderer rend in itemObjectCopy.GetComponentsInChildren<Renderer>()){
//					rend.material.shader = focusedShader;
//					Color trans = rend.material.color;
//					trans.a = .5f;
//					rend.material.color = trans;
//					
//				}
//			}
//
//			//if we're selecting a room corner-mover thing
//			if(this.gameObject.name != "TileMap") tilemapcont.suppressDragSelecting = true;
//			else tilemapcont.suppressDragSelecting = false;
//			
//			//if user wants to cancel the drag
//			if (Input.GetKeyDown (KeyCode.Escape) || Input.GetMouseButton (1)) {
//				Destroy (itemObjectCopy);
//				cancellingMove = true;
//		
//				//break out of while loop
//				break;
//			}
//			
//			Ray ray = UICamera.ScreenPointToRay (Input.mousePosition);
//			RaycastHit hitInfo;
//			
//			Vector3 mouseChange = initMousePos - Input.mousePosition;
//			
//			if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity, draggingLayerMask)) {
//				if (hitInfo.collider.gameObject.name == "TileMap") {
//					int x = Mathf.RoundToInt (hitInfo.point.x / tilemapcont.tileSize);
//					int z = Mathf.RoundToInt (hitInfo.point.z / tilemapcont.tileSize);
//					
//					//if mouse left deadzone
//					if (Math.Abs (mouseChange.x) > mouseDeadZone 
//					    || Math.Abs (mouseChange.y) > mouseDeadZone 
//					    || Math.Abs (mouseChange.z) > mouseDeadZone) {
//						
//						outOfDeadZone = true;
//						
//						//for now y-pos remains as prefab's default.
//						newp = new Vector3 (x * 1.0f, getPosition ().y, z * 1.0f);
//
//						//if copy exists
//						if (itemObjectCopy != null) {
//
//							//Color trans = itemObjectCopy.gameObject.GetComponentInChildren<Renderer>().material.color;
//							//trans.a = 0.5f;
//							//itemObjectCopy.gameObject.GetComponentInChildren<Renderer>().material.SetColor ("_Color", trans);
//							itemObjectCopy.transform.position = new Vector3 (x, getPosition ().y, z);
//							itemObjectCopy.transform.rotation = getRotation();
//						}		
//					}
//				}	
//			}
//			yield return null; 
//		}
//
//		tilemapcont.suppressDragSelecting = false;
//		
//		//destroy the copy
//		Destroy (itemObjectCopy);
//		
//		//if move was cancelled, we don't perform an update on the item object's position
//		if (!cancellingMove) {
//			Vector3 pos = this.gameObject.transform.root.position;
//
//			MapData.dragObject(this.gameObject, pos, newp-pos);
//			tilemapcont.deselect(pos);
//			tilemapcont.selectTile(newp);
//		}
//		inMouseCheck = false;
//	}
//
//	public Vector3 getPosition(){
//		return this.gameObject.transform.position;
//	}
//
//	public Quaternion getRotation(){
//		return this.gameObject.transform.rotation;
//	}
//
//	public string getName(){
//		return this.gameObject.name;
//	}
//	
//	public void setName(string s){
//		this.gameObject.name = s;
//	}
//	
//	public GameObject getGameObject(){
//		return this.gameObject;
//	}
}
