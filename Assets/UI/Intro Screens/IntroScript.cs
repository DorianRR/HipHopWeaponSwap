using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {

	public GameObject Title;
	public GameObject Controls;
	public GameObject PressJoin1;
	public GameObject PressJoin2;
	public GameObject OK1;
	public GameObject OK2;

	private bool P1 = false;
	private bool P2 = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Yell_P1") > 0) {
			if (P2 == false) {
				Title.SetActive (false);
				Controls.SetActive (true);
			}
			PressJoin1.SetActive (false);
			OK1.SetActive (true);
			P1 = true;
		}
		if (Input.GetAxis ("Yell_P2") > 0) {
			if (P1 == false) {
				Title.SetActive (false);
				Controls.SetActive (true);
			}
			PressJoin2.SetActive (false);
			OK2.SetActive (true);
			P2 = true;
		}
		if (P1 && P2) {
			SceneManager.LoadScene (1);
		}
				
	}
}
