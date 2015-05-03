//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIButtonSetup : MonoBehaviour {
	void Start() {
		Button btn;
		CameraMovement move = Camera.main.GetComponent<CameraMovement>();

		btn = GameObject.Find("Button_Room").GetComponent("Button") as Button;
		btn.onClick.AddListener(() => {
			Camera.main.GetComponent<TileMapController>().fillInRoom();
		});
		
		btn = GameObject.Find("Button_ZoomOut").GetComponent("Button") as Button;
		btn.onClick.AddListener(() => {
			move.zoomCamIn();
		});

		btn = GameObject.Find("Button_ZoomIn").GetComponent("Button") as Button;
		btn.onClick.AddListener(() => {
			move.zoomCamOut();
		
		});

		btn = GameObject.Find("Button_ModeToggle").GetComponent("Button") as Button;
		btn.onClick.AddListener(() => {
			move.changeModes();
		});

		btn = GameObject.Find("Button_Delete").GetComponent("Button") as Button;
		btn.onClick.AddListener(() => {
			MapData.delete();
		});

		btn = GameObject.Find("Button_CameraToggle").GetComponent("Button") as Button;
		btn.onClick.AddListener(() => {
			move.toggleCamera();
		});
	}
}
