using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainControl : MonoBehaviour {

	//public ball prefab
	public GameObject ball1Prefab;

	//system public
	public bool resetsw = false;

	//system local
	int ballnum = 0;

	//local
	int cnt=0;
	Vector3	tcS,tcE;

	GameObject stsDisp;
	Text stsDispText;
	AudioSource[] aud;

	//local const
	const float swmv = 200;
	const float tpmv = 60;


	// Use this for initialization
	void Start () {
		//init
		resetsw = false;

		//start 1ball generate
		instBall (0,0);

		//text cash
		stsDisp = GameObject.Find ("stsDisp");
		stsDispText = stsDisp.GetComponent<Text> ();

		//audio
		aud = GetComponents<AudioSource>();

		//bgm
		aud[0].Play();
	}
	
	// Update is called once per frame
	void Update () {
		//tap/swipe検出
		if (Input.GetMouseButtonDown (0)) {
			tcS = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		}
		if (Input.GetMouseButtonUp (0)) {
			tcE = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

			float mvx = tcE.x - tcS.x;
			float mvy = tcE.y - tcS.y;

			if( (Mathf.Abs(mvx) > swmv) || (Mathf.Abs(mvy) > swmv) ){
				//swipe
				Debug.Log("swipe");
				//reset
				resetsw = true;	//reset中もset reset解除直後も問題なし
				aud[1].Play();
			}else{
				if ((Mathf.Abs (mvx) < tpmv) && (Mathf.Abs (mvy) < tpmv)) {
					//tap
					Debug.Log ("Tap");
					//tap位置 ball generate
					if (resetsw == false) {	//reset中は生成しない
						Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						this.instBall (pos.x, pos.y);
					}
				}
			}
		}

		//reset
		if ((resetsw == true) && (ballnum == 0)) {
			//reset解除
			resetsw = false;
		}

		//interval
		cnt++;
		if (cnt > 5) {
			cnt = 0;
			//fps
			stsDispText.text = "tap : generate object\nswipe : reset\nobjects : " + ballnum.ToString("D");
		}
	}

	//ball複数instance生成
	void instBall(float x, float y) {
		int cnt = Random.Range (7, 11);
		for (int i = 0; i < cnt; i++) {
			GameObject go = Instantiate (ball1Prefab) as GameObject;
			go.transform.position = new Vector3 (x, y, 0);
		}
	}

	//public

	//ball number inc/dec
	public void incBall(){
		ballnum++;
	}
	public void decBall(){
		ballnum--;
	}

}
