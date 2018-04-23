using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour {

	void Awake() {
		QualitySettings.vSyncCount = 0; // VSyncをOFFにする
		Application.targetFrameRate = 30;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			SceneManager.LoadScene ("ball1");
		}

	}
}
