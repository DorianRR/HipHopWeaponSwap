﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseScreenScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Yell_P1") > 0 || Input.GetAxis ("Yell_P2") > 0) {
			SceneManager.LoadScene (1);
		}
		if (Input.GetAxis ("X_Button") > 0) {
			Debug.Log ("Quit Recieved");
			Application.Quit ();
		}
	}
}
