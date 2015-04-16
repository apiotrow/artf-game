using UnityEngine;
using System;
using System.Collections;

public class RoomDraggingObject : ClickEvent {
	public LayerMask draggingLayerMask;
	Camera UICamera;
	TileMapController tilemapcont;

	void Start() {
		draggingLayerMask = LayerMask.GetMask("Walls");
		UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
		tilemapcont = GameObject.Find("TileMap").GetComponent("TileMapController") as TileMapController;
	}
		
	public override IEnumerator onClick(Vector3 initPosition) {
		if(!Mode.isRoomMode()) {
			return false;
		}

		Ray ray = UICamera.ScreenPointToRay(initPosition);
		float distance;
		Global.ground.Raycast(ray, out distance);

		Vector3 origin = ray.GetPoint(distance).Round();

		//for the ghost-duplicate
		Vector3 newp = this.gameObject.transform.position;
		tilemapcont.suppressDragSelecting = true;
		while(Input.GetMouseButton(0)) { 
			//if user wants to cancel the drag
			if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(1)) {
				Debug.Log("Cancel");
				return false;
			}
			
			ray = UICamera.ScreenPointToRay(Input.mousePosition);
			Global.ground.Raycast(ray, out distance);
			
			Vector3 mouseChange = initPosition - Input.mousePosition;

			float x = Mathf.Round(ray.GetPoint(distance).x);
			float z = Mathf.Round(ray.GetPoint(distance).z);
					
			//if mouse left deadzone
			if(Math.Abs(mouseChange.x) > Global.mouseDeadZone 
				|| Math.Abs(mouseChange.y) > Global.mouseDeadZone 
				|| Math.Abs(mouseChange.z) > Global.mouseDeadZone) {

				//for now y-pos remains as prefab's default.
				newp = new Vector3(x, getPosition().y, z);
			}	

			yield return null; 
		}
		tilemapcont.suppressDragSelecting = false;
		tilemapcont.deselect(origin);
		Debug.Log(origin + ", " + newp);
		MapData.moveRoom(origin, newp);
		tilemapcont.selectTile(newp);
	}

	public Vector3 getPosition() {
		return this.gameObject.transform.root.position.Round();
	}
	
	public Quaternion getRotation() {
		return this.gameObject.transform.rotation;
	}
}
