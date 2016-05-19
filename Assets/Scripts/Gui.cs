using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour {

	void OnGUI()
    {
//		GUI.Box (new Rect (0,0,100,50), "Top-left");
//		GUI.Box (new Rect (Screen.width - 100,0,100,50), "Top-right");
		GUI.Box (new Rect (0, Screen.height - 75, 275, 800), "Health");
//		GUI.Box (new Rect (Screen.width - 100,Screen.height - 50,100,50), "Bottom-right");
	}

}