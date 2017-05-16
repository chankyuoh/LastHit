using UnityEngine;
using UnityEngine.UI;

public class SoldierShooting : MonoBehaviour
{
	public int m_PlayerNumber;  
    public Rigidbody m_Shell;    
	public Rigidbody m_Rocket;
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;



	private string m_UltButton;
    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;
	private bool m_UltUsed;


    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;
		if (m_PlayerNumber == 1) {
			m_UltButton = "left shift";
		} else if (m_PlayerNumber == 2) {
			m_UltButton = "right shift";
		}
		m_UltUsed = false;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;

    }
    

    private void Update()
    {
		m_AimSlider.value = m_MinLaunchForce;
		if ((m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)) {
			//at max charge, haven't fired yet
			m_CurrentLaunchForce = m_MaxLaunchForce;
			if (Input.GetKeyDown (m_UltButton)) {
				UltFire ();
			} else 
			{
				Fire ();
			}


		} else if (Input.GetButtonDown (m_FireButton) || (Input.GetKeyDown(m_UltButton) && !m_UltUsed)) {
			//have we pressed fired for the first time?
			m_Fired = false;
			m_CurrentLaunchForce = m_MinLaunchForce;
			//m_ShootingAudio.clip = m_ChargingClip;
			//m_ShootingAudio.Play ();

		} else if ((Input.GetButton (m_FireButton) || (Input.GetKey(m_UltButton) && !m_UltUsed)) && !m_Fired) {
			// Holding the fire button, not yet fired
			m_CurrentLaunchForce += m_ChargeSpeed*Time.deltaTime;
			m_AimSlider.value = m_CurrentLaunchForce;
			//			anim.Play ("demo_combat_shoot");
		} else if ((Input.GetButtonUp (m_FireButton) || (Input.GetKeyUp(m_UltButton) && !m_UltUsed) && !m_Fired)) {
			// released button
			if (Input.GetKeyUp (m_UltButton)) {
				UltFire ();
			} 
			else {
				Fire ();
			}
		}
    }





    private void Fire()
    {

        // Instantiate and launch the shell.
		m_Fired = true;
		Rigidbody shellInstance = Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

		shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;
		shellInstance.gameObject.name = "shell" + m_PlayerNumber;

//		m_ShootingAudio.clip = m_FireClip;
//		m_ShootingAudio.Play ();
		m_CurrentLaunchForce = m_MinLaunchForce;

    }

	private void UltFire()
	{
		// Instantiate and launch the shell.
		m_Fired = true;
		Rigidbody rocketInstance = Instantiate (m_Rocket, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

		rocketInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;
		rocketInstance.gameObject.name = "shell" + m_PlayerNumber;

//		m_ShootingAudio.clip = m_FireClip;
//		m_ShootingAudio.Play ();
		m_CurrentLaunchForce = m_MinLaunchForce;

		m_UltUsed = true;



	}
}