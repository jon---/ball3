using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameControl : MonoBehaviour {

	public const bool sw_fpsDisp = true;

	GameObject fpsDisp;
	float delta;
	float fpscnt=0.0f;

	Text fpsDispText;

	void Awake() {
		QualitySettings.vSyncCount = 0; // VSyncをOFFにする
		Application.targetFrameRate = 30;
	}

	// Use this for initialization
	void Start () {
		fpsDisp = GameObject.Find ("fpsDisp");
		fpsDispText = fpsDisp.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		//fps
		if (sw_fpsDisp == true) {
			fpscnt++;
			delta = delta + Time.deltaTime;
			if (delta >= 1.0f) {
				delta = 0;
				//fps
				fpsDispText.text = "fps " + fpscnt.ToString("F1");	//整数だけだがひとまず
				fpscnt = 0;
			}
		} else {
			//
			fpsDispText.text = "fps no cnt";
		}
		
	}
}
