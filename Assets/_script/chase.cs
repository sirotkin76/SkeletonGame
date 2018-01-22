using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class chase : MonoBehaviour {
	public Transform player;
	static Animator anim;

	public Slider healthbar;
	public float distans;
	public GameObject[] wayPoints;
	public Transform head;
	int currentWP = 0;
	public float rotSpeed = 0.2f;
	public float speed = 1.5f;
	float accuracyWP = 1.0f;

	public bool attacking;
	public float timeAnimAttack = 2.1f;
	public float timeAttack;

	public string state = "patrol";
	public bool meleeHit = false;

	public bool deathMoney = false;
	public GameObject prefab;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		timeAttack = timeAnimAttack;
	}
	
	// Update is called once per frame
	void Update () {

		if (GetComponent<detectHit>().health <= 0) {
			if (!deathMoney) {
				Instantiate(prefab, this.transform.position + Vector3.up, this.transform.rotation);
				deathMoney = true;
			}

			anim.SetBool("isDeath", true);
			anim.SetBool("isAttacking", false);
			anim.SetBool("isIdle", false);
			anim.SetBool("isRun", false);
			anim.SetBool("isWalking", false);
			
			return;
		}

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

			if (direction.magnitude > distans) {
				this.transform.Translate(0, 0, speed * Time.deltaTime);
				anim.SetBool("isWalking", true);
				anim.SetBool("isAttacking", false);

				timeAttack = timeAnimAttack;
				attacking = false;

				//anim.SetBool("isIdle", false);
			} else {

				if (meleeHit) {
					anim.SetBool("isWalking", false);
					anim.SetBool("isAttacking", true);

					timeAttack -= Time.deltaTime;

					if (timeAttack <= 0) {
						meleeHit = false;
						attacking = true;
						timeAttack = timeAnimAttack;
					}

				} else 
					meleeHit = true;
			}

		} else {
				anim.SetBool("isWalking", true);
				anim.SetBool("isAttacking", false);
				//anim.SetBool("isIdle", true);
				state = "patrol";
			}
	}
}
