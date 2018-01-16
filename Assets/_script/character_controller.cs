using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_controller : MonoBehaviour {

	static Animator anim;
	float speed;
	public float speedWalk;
	public float speedRun;
	bool dHit;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		Cursor.lockState = CursorLockMode.Locked;
		speed = speedWalk;
	}
	
	// Update is called once per frame
	void Update () {

		dHit = GetComponent<detectHit>().death;
		if (dHit) {
			return;
		};
		
		float translation = Input.GetAxis("Vertical") * speed;
		float straffe = Input.GetAxis("Horizontal") * speed;

		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;

		transform.Translate(0, 0, translation);

		if (Input.GetKeyDown(KeyCode.LeftShift)){
			anim.SetBool("isRun", true);
			this.GetComponent<character_controller>().speed = speedRun;
		}

		if (Input.GetKeyUp(KeyCode.LeftShift)){
			anim.SetBool("isRun", false);
			this.GetComponent<character_controller>().speed = speedWalk;
		}

		if (Input.GetButton("Fire1")) {
			anim.SetBool("isAttacking", true);
		} else 
			anim.SetBool("isAttacking", false);

		if (translation != 0) {
			anim.SetBool("isWalking", true);
			anim.SetBool("isIdle", false);
		} else {
			anim.SetBool("isWalking", false);
			anim.SetBool("isIdle", true);
		}

		if (Input.GetKeyDown("escape")) 
			Cursor.lockState = CursorLockMode.None;

	}
}
