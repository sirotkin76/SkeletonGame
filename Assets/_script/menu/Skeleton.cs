using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour {

	Animator anim;

	public Transform[] wPoints;
	public float speed;
	

	float timer;
	int timeRandom;
	int curPoint = 0;
	Vector3 direction;

	float timeStand = 20f;
	float changeStand;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		changeStand = timeStand;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (wPoints.Length == 0) {

			changeStand -= Time.deltaTime;

			if (changeStand <= 0) {
				anim.SetBool("isStand", false);
				anim.SetBool("isWalk", false);

				anim.SetBool("isIdle", true);

				Debug.Log("2 sk - " + this.name + " --- " + changeStand);

				changeStand = timeStand;
			} else {
				anim.SetBool("isIdle", false);
				anim.SetBool("isWalk", false);

				anim.SetBool("isStand", true);
				

				Debug.Log("1 sk - " + this.name + " --- " + changeStand);
			} 

		} else {
			anim.SetBool("isWalk", true);
			anim.SetBool("isIdle", false);
			anim.SetBool("isStand", false);

			if (Vector3.Distance(wPoints[curPoint].transform.position, transform.position) < 0.1f) {
				curPoint ++;

				if (curPoint >= wPoints.Length)
					curPoint = 0;
			}

			direction = wPoints[curPoint].transform.position - transform.position;
			this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 5f * Time.deltaTime);
			this.transform.Translate(0, 0, Time.deltaTime * speed);
		}
	}
}
