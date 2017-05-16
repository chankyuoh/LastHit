using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAnimation : MonoBehaviour {

	public Animation anim;
	public int m_PlayerNumber;
	private float m_MovementInputValue;
	private string m_MovementAxisName;
	private string m_FireButton;  

	// Use this for initialization
	void Start () {
		m_MovementAxisName = "Vertical" + m_PlayerNumber;
		m_FireButton = "Fire" + m_PlayerNumber;
		anim = GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
		m_MovementInputValue = Input.GetAxis(m_MovementAxisName);

		if (Input.GetButtonDown (m_FireButton)){	
			anim.Play ("demo_combat_shoot");

		}
		else if (!Input.GetButtonDown (m_FireButton) && m_MovementInputValue == 0) {
			anim.Play ("demo_combat_idle");
		} 
		else {
			anim.Play ("demo_combat_run");
		}


	}

}
