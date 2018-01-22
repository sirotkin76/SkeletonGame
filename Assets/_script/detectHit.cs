using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class detectHit : MonoBehaviour {

	public Slider healthbar;
	public float health;
	public Animator anim;
	public bool death;
	public bool attack = false;
	public bool firstHit = false;
	public float hp;

	public GameObject VisualHP;

	void OnTriggerStay (Collider other) {
		if (other.gameObject.tag == "Well") {
			if (this.gameObject.tag == "Player") {
				if(health < 100) {
					hp += Time.deltaTime;

					if (hp > 1) {
						hp = 0;
						health += 1;
					}
				}

				VisualHP.SetActive(true);
			}
			
			return;
		} 
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Well") {
			if (this.gameObject.tag == "Player") {
				VisualHP.SetActive(false);
			}
		}
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "Player")
			return;

		bool eDeath = other.gameObject.GetComponentInParent<detectHit>().death;

		if (other.gameObject.tag == this.gameObject.tag  ) return;

		if (other.gameObject.tag == "Player" && this.gameObject.tag != "Player") {
			var charCont = other.gameObject.GetComponentInParent<character_controller>();

			if (charCont.eventAttack) {
				attack = true;

				if (!charCont.meleeFirstHit) {
					firstHit = false;
					other.gameObject.GetComponentInParent<character_controller>().meleeFirstHit = true;
				}

			} else {
				attack = false;
			}

		} else {
			if (other.gameObject.GetComponentInParent<chase>().attacking){
				attack = true;
				firstHit = false;
				other.gameObject.GetComponentInParent<chase>().attacking = false;
			} else {
				attack = false;
				firstHit = true;
			}
		}
		
		if (!eDeath && attack && !firstHit ) {
			firstHit = true;
			health -= 105;

			if (health < 0) {
				health = 0 ;
			} 

			anim.SetTrigger("isDamage");

			if (this.gameObject.tag != "Player") {
				this.GetComponentInParent<chase>().state = "pursuing";
			}
		}

		if (health <= 0) {
			death = true;
			anim.SetBool("isDeath", death);
		} else {
			death = false;
			anim.SetBool("isDeath", death);
		}
	}


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
