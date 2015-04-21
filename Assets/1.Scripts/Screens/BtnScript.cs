﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BtnScript : MonoBehaviour {
    public void SelectTxt() {
        Text[] txtChild = this.GetComponentsInChildren<Text>();
        foreach (Text child in txtChild)
        {
            child.color = new Color32(252, 208, 0, 255);
        }
    }

    public void DeselectTxt()
    {
        Text[] txtChild = this.GetComponentsInChildren<Text>();
        foreach (Text child in txtChild)
        {
            
            child.color = new Color32(152, 213, 217, 255);
        }
    }
}
