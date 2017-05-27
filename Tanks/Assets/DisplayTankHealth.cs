using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTankHealth : MonoBehaviour {
	private SoldierHealth m_Health;
	[HideInInspector] public GameObject m_Instance;   
	// Use this for initialization
	void Start () {
		m_Health = m_Instance.GetComponent<SoldierHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
