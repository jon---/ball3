using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball1 : MonoBehaviour {

	//system
	int cnt;

	//ball
	float xx;	//x変化量
	float yy;	//y変化量
	float ballxs,ballys;	//x,y size
	int stopcnt;	//start時stop timer
	float colR,colG,colB,colA;	//rgba 
	float colRR,colGG,colBB,colAA;	//rgba変化量
	float scl;
	bool doadf;
	//component cash
	Transform cashTransform;
	AudioSource se;
	Animator animt;
	SpriteRenderer sr;
	Rigidbody2D r2;

	//wall
	GameObject wall_u,wall_d,wall_l,wall_r;	//wall object
	float wall_uys,wall_dys,wall_lxs,wall_rxs;	//wall size
	Transform cashTransformWall_u,cashTransformWall_d,cashTransformWall_l,cashTransformWall_r;	//transform cash

	//system
	GameObject mainCtr;
	mainControl mc;

	// Use this for initialization
	void Start () {
		//system init
		cnt = 0;

		//system
		mainCtr = GameObject.Find ("mainControl");
		mc = mainCtr.GetComponent<mainControl> ();
		//system ball num inc
		mc.incBall ();

		//ball xy init
//		xx = 0.1f;	//固定の場合
//		yy = 0.1f;	//固定の場合
		xx = 0.05f + Random.Range(0.0f, 0.1f);
		yy = 0.05f + Random.Range(0.0f, 0.1f);
		if (Random.Range(0,2) < 1) {
			xx = xx - (xx * 2);
		}
		if (Random.Range(0,2) < 1) {
			yy = yy - (yy * 2);
		}

		//ball stop time init
		stopcnt = 40;

		//ball size
		ballxs = (this.GetComponent<SpriteRenderer> ().bounds.size.x)/2;
		ballys = (this.GetComponent<SpriteRenderer> ().bounds.size.y)/2;

		//ball transform cash
		cashTransform = transform;

		//ball audio
		se = GetComponent<AudioSource>();

		//ball anim
		animt = GetComponent<Animator>();
		animt.speed = 0.8f;

		//ball sprite renderer
		sr = GetComponent<SpriteRenderer>();

		//ball rigidbody2d
		r2 = GetComponent<Rigidbody2D>();

		//collider at stop
		r2.bodyType = RigidbodyType2D.Kinematic;

		//ball scale
		scl = 1.0f;

		//ball color init
		colR = 1.0f;
		colG = 1.0f;
		colB = 1.0f;
		colA = 1.0f;
		colRR = 0.01f + Random.Range(0.0f, 0.03f);
		colGG = 0.01f + Random.Range(0.0f, 0.03f);
		colBB = 0.01f + Random.Range(0.0f, 0.03f);
		colAA = 0.01f + Random.Range(0.0f, 0.03f);
		if (Random.Range(0,2) < 1) {
			colRR = colRR - (colRR * 2);
		}
		if (Random.Range(0,2) < 1) {
			colGG = colGG - (colGG * 2);
		}
		if (Random.Range(0,2) < 1) {
			colBB = colBB - (colBB * 2);
		}
		if (Random.Range(0,2) < 1) {
			colAA = colAA - (colAA * 2);
		}

		//adforce doing flag
		doadf = false;

		//wall
		this.wall_u = GameObject.Find("wall_u");
		this.wall_d = GameObject.Find("wall_d");
		this.wall_l = GameObject.Find("wall_l");
		this.wall_r = GameObject.Find("wall_r");

		//wall size
		wall_uys = (wall_u.GetComponent<SpriteRenderer> ().bounds.size.y)/2;
		wall_dys = (wall_d.GetComponent<SpriteRenderer> ().bounds.size.y)/2;
		wall_lxs = (wall_l.GetComponent<SpriteRenderer> ().bounds.size.x)/2;
		wall_rxs = (wall_r.GetComponent<SpriteRenderer> ().bounds.size.x)/2;

		//wall transform cash
		cashTransformWall_u = wall_u.transform;
		cashTransformWall_d = wall_d.transform;
		cashTransformWall_l = wall_l.transform;
		cashTransformWall_r = wall_r.transform;
	}
	
	// Update is called once per frame
	void Update () {
		//interval
		cnt++;
		if (cnt > 0) {
			cnt = 0;

			//ball stop time  at start
			if (stopcnt >= 1) {
				stopcnt--;
				return;
			} else if (stopcnt == 0) {
				//collider at start
				r2.bodyType = RigidbodyType2D.Dynamic;
			}

			//ball move
			if (mc.resetsw == false) {	//reset中は移動停止
//l				cashTransform.Translate (xx, yy, 0);
				if (doadf == false) {
					doadf = true;
					r2.AddForce (new Vector2 (xx, yy));
				}
			}

//l
/* 
			//反転
			bool rvs = false;

			//x反転
			if (( (cashTransform.position.x + ballxs) > (cashTransformWall_r.position.x - wall_rxs) ) ||
				( (cashTransform.position.x - ballxs) < (cashTransformWall_l.position.x + wall_lxs) ) ) {
				xx = xx - (xx * 2);
				se.Play();
				rvs = true;
			}

			//y反転
			if (( (cashTransform.position.y + ballys) > (cashTransformWall_u.position.y - wall_uys) ) ||
				( (cashTransform.position.y - ballys) < (cashTransformWall_d.position.y + wall_dys) ) ){
				yy = yy - (yy * 2);
				se.Play();
				rvs = true;
			}

			//反転animation
			if (rvs == true) {
				animt.speed = 3.5f;
				animt.SetTrigger ("bound");
			}
*/

			//色変化
			colR = colR + colRR;
			if ( (colR > 2.4f) || (colR < 0.6f) ) {
				colRR = colRR - (colRR * 2);
			}
			colG = colG + colGG;
			if ( (colG > 2.4f) || (colG < 0.6f) ) {
				colGG = colGG - (colGG * 2);
			}
			colB = colB + colBB;
			if ( (colB > 2.4f) || (colB < 0.6f) ) {
				colBB = colBB - (colBB * 2);
			}
			colA = colA + colAA;
			if ( (colA > 2.4f) || (colA < 0.6f) ) {
				colAA = colAA - (colAA * 2);
			}
			sr.color = new Color (colR, colG, colB, colA);

			//scale (reset時)
			if (mc.resetsw == true) {
				r2.velocity = Vector2.zero;
				cashTransform.localScale = new Vector3 (scl, scl, 1);
				scl = scl - 0.015f;
				if (scl < 0.2f) {
					mc.decBall ();			//ball dec
					Destroy (gameObject);	//this Destroy
				}
			}
		}

	}

	//collision
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "wall") {
			//collision wall
			Debug.Log ("Collision wall!");
			se.Play();
			animt.speed = 3.5f;
			animt.SetTrigger ("bound");

		} else if (coll.gameObject.tag == "ball1") {
			//collision ball
			Debug.Log ("Collision ball!");
		} else {
			//collision other
			Debug.Log ("Collision anything");
		}
	}
}
