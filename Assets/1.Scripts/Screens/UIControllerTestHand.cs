﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIControllerTestHand : MonoBehaviour {
    public Controls controls;

    private InputField fieldCharName;
    private string tmpCharName;
    private float pressTime;
    private string currBtn = "";
    private string prevBtn = "";

    private Button[,] menu;
    private int menuVertLoc = 0;
    private int menuHoriLoc = 0;
    private bool menuMoved = false;
    private int vertSize = 3;
    private int horiSize = 3;

	// Use this for initialization
	void Start () {
        fieldCharName = GameObject.Find("FieldCharName").GetComponent<InputField>();

        menu = new Button[vertSize, horiSize];

        // top row
        menu[0, 0] = GameObject.Find("BtnABC").GetComponent<Button>();
        menu[0, 1] = GameObject.Find("BtnDEF").GetComponent<Button>();
        menu[0, 2] = GameObject.Find("BtnGHI").GetComponent<Button>();

        menu[0, 0].onClick.AddListener(() =>
        {
            currBtn = "ABC";

            if (currBtn != prevBtn)
            {
                pressTime = Time.time;
                tmpCharName = fieldCharName.text;
                fieldCharName.text = tmpCharName + "a";
            }
            else
            {
                if ((Time.time - pressTime) < 3.0)
                {
                    fieldCharName.text = tmpCharName + "b";
                    pressTime = Time.time;
                }
                else
                {
                    pressTime = Time.time;
                    tmpCharName = fieldCharName.text;
                    fieldCharName.text = tmpCharName + "a";
                }
            }

            prevBtn = "ABC";
        }
        );

        menu[0, 1].onClick.AddListener(() =>
        {
            Debug.Log("DEF");
            fieldCharName.text += "d";
        }
        );

        menu[0, 2].onClick.AddListener(() =>
        {
            Debug.Log("GHI");
            fieldCharName.text += "g";
        }
        );

        // middle row
        menu[1, 0] = GameObject.Find("BtnJKL").GetComponent<Button>();
        menu[1, 1] = GameObject.Find("BtnMNO").GetComponent<Button>();
        menu[1, 2] = GameObject.Find("BtnPQRS").GetComponent<Button>();

        menu[1, 0].onClick.AddListener(() =>
        {
            Debug.Log("JKL");
            fieldCharName.text += "j";
        }
        );

        menu[1, 1].onClick.AddListener(() =>
        {
            Debug.Log("MNO");
            fieldCharName.text += "m";
        }
        );

        menu[1, 2].onClick.AddListener(() =>
        {
            Debug.Log("PQRS");
            fieldCharName.text += "p";
        }
        );

        // bottom row
        menu[2, 0] = GameObject.Find("BtnTUV").GetComponent<Button>();
        menu[2, 1] = GameObject.Find("BtnWXYZ").GetComponent<Button>();
        menu[2, 2] = GameObject.Find("BtnNum").GetComponent<Button>();

        menu[2, 0].onClick.AddListener(() =>
        {
            Debug.Log("TUV");
            fieldCharName.text += "t";
        }
        );

        menu[2, 1].onClick.AddListener(() =>
        {
            Debug.Log("WXYZ");
            fieldCharName.text += "w";
        }
        );

        menu[2, 2].onClick.AddListener(() =>
        {
            Debug.Log("Num");
        }
        );

        menu[0, 0].Select();
	}

    void stickUp ()
    {
        if (menuMoved == false)
        {
            menuMoved = true;
            --menuVertLoc;
            if (menuVertLoc < 0)
                menuVertLoc = vertSize - 1;
            //Debug.Log(menuVertLoc);
        }
    }

    void stickDown()
    {
        if (menuMoved == false)
        {
            menuMoved = true;
            ++menuVertLoc;
            if (menuVertLoc > (vertSize - 1))
                menuVertLoc = 0;
            //Debug.Log(menuVertLoc);
        }
    }

    void stickRight()
    {
        if (menuMoved == false)
        {
            menuMoved = true;
            ++menuHoriLoc;
            if (menuHoriLoc > (horiSize - 1))
                menuHoriLoc = 0;
            //Debug.Log(menuHoriLoc);
        }
    }

    void stickLeft()
    {
        if (menuMoved == false)
        {
            menuMoved = true;
            --menuHoriLoc;
            if (menuHoriLoc < 0)
                menuHoriLoc = horiSize - 1;
            //Debug.Log(menuHoriLoc);
        }
    }

    void stickNeutral()
    {
        menuMoved = false;
        //Debug.Log("nair");
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(Input.GetAxisRaw(controls.vert));
        //Debug.Log(Time.time);

        if (Input.GetAxisRaw(controls.vert) > 0) {
            stickUp();
            menu[menuVertLoc, menuHoriLoc].Select();
        }
        else if (Input.GetAxisRaw(controls.vert) < 0) {
            stickDown();
            menu[menuVertLoc, menuHoriLoc].Select();
        }

        if (Input.GetAxisRaw(controls.hori) > 0)
        {
            stickRight();
            menu[menuVertLoc, menuHoriLoc].Select();
        }
        else if (Input.GetAxisRaw(controls.hori) < 0)
        {
            stickLeft();
            menu[menuVertLoc, menuHoriLoc].Select();
        }

        // prevent inputting stick controls every frame by requiring player to set stick to neutral to move again
        if (Input.GetAxisRaw(controls.vert) == 0 && Input.GetAxisRaw(controls.hori) == 0)
        {
            stickNeutral();
        }
	}
}