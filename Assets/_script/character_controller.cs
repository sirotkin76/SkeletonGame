using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class character_controller : MonoBehaviour {

	static Animator anim;
	float speed;
	public float speedWalk;
	public float speedRun;
	bool dHit;
	public Slider slider;

	float timeAnimAttack = 2.5f;
	float timeAttack;
	public bool eventAttack = false;
	public bool meleeFirstHit = false;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		Cursor.lockState = CursorLockMode.Locked;
		speed = speedWalk;
		timeAttack = timeAnimAttack;
	}
	
	// Update is called once per frame
	void Update () {

		dHit = GetComponent<detectHit>().death;
		if (dHit) {
			return;
		};

		slider.value = GetComponent<detectHit>().health;
		
		float translation = Input.GetAxis("Vertical") * speed;
		float straffe = Input.GetAxis("Horizontal") * speed;

		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;

		if (!eventAttack) {
			transform.Translate(0, 0, translation);
		}

		if (Input.GetKey(KeyCode.LeftShift)){
			
			if (translation > 0) {
				anim.SetBool("isRun", true);
				this.GetComponent<character_controller>().speed = speedRun;
			} 
			else {
				anim.SetBool("isRun", false);
				this.GetComponent<character_controller>().speed = speedWalk;
			}
		}

		if (Input.GetKeyUp(KeyCode.LeftShift)){
			anim.SetBool("isRun", false);
			this.GetComponent<character_controller>().speed = speedWalk;
		}

		if (Input.GetButton("Fire1") && !eventAttack) {
			anim.SetBool("isAttacking", true);
			translation = 0;

			eventAttack = true;
		} 
		else {
			if (eventAttack) {
				timeAttack -= Time.deltaTime;

				if (timeAttack < 0) {
					meleeFirstHit = false;
					eventAttack = false;
				}
			} 
			else {
				timeAttack = timeAnimAttack;
				anim.SetBool("isAttacking", false);
				eventAttack = false;
			}
		}

		if (translation != 0) {
			anim.SetBool("isWalking", true);
			anim.SetBool("isIdle", false);
		} else {
			anim.SetBool("isWalking", false);
			anim.SetBool("isIdle", true);
		}

		if (Input.GetKeyDown("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}

		if (Input.GetKeyDown(KeyCode.F1)) Application.LoadLevel("menu");
		if (Input.GetKeyDown(KeyCode.F2)) Application.LoadLevel("main");

	}
}
