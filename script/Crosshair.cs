using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;


public class Crosshair : MonoBehaviour
{
    public Texture2D pricel;

    public void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width / 2, Screen.height / 2, 40, 40), pricel);
    }
}
