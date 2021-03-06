﻿using UnityEngine;
using UnityEngine.UI;

public class SoldierHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;          
    public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;    
    public GameObject m_ExplosionPrefab;
	public int m_PlayerNumber;


	public static bool m_didLastHit1;
	public static bool m_didLastHit2;
	public static float m_tankHealth;
    
    private AudioSource m_ExplosionAudio;          
    private ParticleSystem m_ExplosionParticles;   
    public float m_CurrentHealth;  
    private bool m_Dead;            


    private void Awake()
    {
//		m_didLastHit = false;
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }
		


	public void TakeDamage(float amount, string shellName)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
		m_CurrentHealth -= amount;
		SetHealthUI ();
		if (m_CurrentHealth <= 0f && !m_Dead) {
			if (this.tag == "BigTank") {
				if (shellName == "shell1") {
					print ("PLAYER 1 WON!!");
					m_didLastHit1 = true;
				} else if (shellName == "shell2") {
					print ("PLAYER 2 WON!!");
					m_didLastHit2 = true;
				} else {
					print ("what happen");
				}
			}

			OnDeath ();
		}
    }


    public void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
		m_Slider.value = m_CurrentHealth;

		if (m_PlayerNumber == 1) {
			m_FillImage.color = Color.blue;
		} else if (m_PlayerNumber == 2) {
			m_FillImage.color = Color.red;
		} else {
			m_FillImage.color = Color.Lerp (m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
		}

		if (m_PlayerNumber == 3 || this.tag == "BigTank"){
			m_tankHealth = m_CurrentHealth;
		}


    }


    public void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.

		m_Dead = true;
		m_ExplosionParticles.transform.position = transform.position;
		m_ExplosionParticles.gameObject.SetActive (true);
		m_ExplosionParticles.Play ();
		m_ExplosionAudio.Play ();
		gameObject.SetActive (false);

    }


//
	public bool getDidLastHit1(){
		print ("getLastHit1 is: " + m_didLastHit1);
		return m_didLastHit1;
	}

	public bool getDidLastHit2(){
		print ("getLastHit2 is: " + m_didLastHit2);
		return m_didLastHit2;
	}


		
}