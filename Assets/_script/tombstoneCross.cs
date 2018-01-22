using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tombstoneCross : MonoBehaviour {

	public GameObject enemy;
	public float timeDead;
	float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (enemy.gameObject.GetComponent<detectHit>().health <= 0) {
			
			timer += Time.deltaTime;

			if (timer >= timeDead){
				timer = 0;
				enemy.gameObject.GetComponent<detectHit>().health = 100;
				enemy.gameObject.GetComponent<detectHit>().death = false;
				enemy.gameObject.GetComponent<detectHit>().anim.SetBool("isDeath", false);
				enemy.gameObject.GetComponent<chase>().deathMoney = false;
			}

		}
	}
}
