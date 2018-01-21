using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour {

	public void LoadOneScene(){
		Application.LoadLevel ("main");
	}

	public void Exit() {
		Application.Quit();
	}
}
