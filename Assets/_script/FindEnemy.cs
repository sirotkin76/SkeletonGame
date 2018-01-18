using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FindEnemy : MonoBehaviour {

	public GameObject enemySlider;
	public Slider eSlider;
	public Camera camera;

	Vector3 target;
	Ray ray;

	// Use this for initialization
	void Start () {
		target = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		RaycastHit hit;
		ray = camera.ScreenPointToRay(Input.mousePosition);

		Vector3 fwd = transform.TransformDirection(Vector3.forward) * 50;
		Vector3 person = transform.position;

		if (Physics.Raycast(ray, out hit)){

			if (hit.collider.tag == "Enemy") {
				
				try {
					eSlider.gameObject.SetActive(true);
					eSlider.value = hit.collider.gameObject.GetComponent<detectHit>().health;
				}
				catch {
					eSlider.gameObject.SetActive(false);
				}


			} else {
				eSlider.gameObject.SetActive(false);
			}

			Debug.DrawRay(person, fwd, Color.green);
			target = hit.collider.gameObject.transform.position;

		} else 
			enemySlider.SetActive(false);
			
	}

	void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(ray);
    }
}
