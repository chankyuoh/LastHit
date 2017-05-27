using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateHealth : MonoBehaviour {
	public Text tankHealthText;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

		if (SoldierHealth.m_tankHealth < 0) {
			tankHealthText.text = "0";
		}
		else{
			tankHealthText.text = SoldierHealth.m_tankHealth.ToString ();
		}
	}
}
