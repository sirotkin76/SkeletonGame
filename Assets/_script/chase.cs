using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class chase : MonoBehaviour {
	public Transform player;
	static Animator anim;

	public Slider healthbar;

	public GameObject[] wayPoints;
	public Transform head;
	int currentWP = 0;
	public float rotSpeed = 0.2f;
	public float speed = 1.5f;
	float accuracyWP = 1.0f;

	public string state = "patrol";

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if (healthbar.value <= 0) return;

		Vector3 direction = player.position - this.transform.position;
		direction.y = 0;
		float angle = Vector3.Angle(direction, head.up);

		if (state == "patrol" && wayPoints.Length > 0) {
			anim.SetBool("isIdle", false);
			anim.SetBool("isWalking", true);
			
			if (Vector3.Distance(wayPoints[currentWP].transform.position, transform.position) < accuracyWP) {
				currentWP++;
				if (currentWP >= wayPoints.Length) {
					currentWP = 0;
				}
			}

			direction = wayPoints[currentWP].transform.position - transform.position;
			this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
			this.transform.Translate(0, 0, Time.deltaTime * speed);

		}

		bool death = player.GetComponent<detectHit>().death;

		if (Vector3.Distance(player.position, this.transform.position) < 10 && (angle < 30 || state == "pursuing") && !death) {
			
			state = "pursuing";
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);

			//anim.SetBool("isIdle", false);

			if (direction.magnitude > 2) {
				this.transform.Translate(0, 0, speed * Time.deltaTime);
				anim.SetBool("isWalking", true);
				anim.SetBool("isAttacking", false);
				//anim.SetBool("isIdle", false);
			} else {
				anim.SetBool("isWalking", false);
				anim.SetBool("isAttacking", true);
				//anim.SetBool("isIdle", false);
			}
		} else {
				anim.SetBool("isWalking", true);
				anim.SetBool("isAttacking", false);
				//anim.SetBool("isIdle", true);
				state = "patrol";
			}
	}
}
