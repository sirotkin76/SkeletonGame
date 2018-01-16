using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class detectHit : MonoBehaviour {

	public Slider healthbar;
	Animator anim;
	public bool death;

	void OnTriggerEnter(Collider other) {

		bool eDeath = other.gameObject.GetComponentInParent<detectHit>().death;

		if (other.gameObject.tag == this.gameObject.tag ) return;
		
		if (!eDeath){
			healthbar.value -= 30;
			anim.SetTrigger("isDamage");

			if (this.gameObject.tag != "Player"){
				this.GetComponentInParent<chase>().state = "pursuing";
			}


		}

		if (healthbar.value <= 0) {
			death = true;
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
