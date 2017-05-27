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
	private bool m_UltFired;
	public bool m_UltUsed;


    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;
		if (m_PlayerNumber == 1) {
			m_UltButton = "ultLeft";
		} else if (m_PlayerNumber == 2) {
			m_UltButton = "ultRight";
		}
		m_UltUsed = false;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;

    }
    

    private void Update()
    {
		m_AimSlider.value = m_MinLaunchForce;
		manageNormalFireInput ();
		manageUltFireInput ();
    }


	private void manageNormalFireInput()
	{
		if ((m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)) {
			//at max charge, haven't fired yet
			m_CurrentLaunchForce = m_MaxLaunchForce;
			Fire ();


		} else if (Input.GetButtonDown (m_FireButton)) {
			//have we pressed fired for the first time?
			m_Fired = false;
			m_CurrentLaunchForce = m_MinLaunchForce;
			//m_ShootingAudio.clip = m_ChargingClip;
			//m_ShootingAudio.Play ();

		} else if ((Input.GetButton (m_FireButton)) && !m_Fired) {
			// Holding the fire button, not yet fired
			m_CurrentLaunchForce += m_ChargeSpeed*Time.deltaTime;
			m_AimSlider.value = m_CurrentLaunchForce;
			//			anim.Play ("demo_combat_shoot");
		} else if ((Input.GetButtonUp (m_FireButton) && !m_Fired)) {
			// released button
			//			if (Input.GetKeyUp (m_UltButton)) {
			//				UltFire ();
			//			} 
			Fire ();
		}
	}

	private void manageUltFireInput()
	{
		if (!m_UltUsed) 
		{
			if ((m_CurrentLaunchForce >= m_MaxLaunchForce && !m_UltFired)) {
				//at max charge, haven't fired yet
				m_CurrentLaunchForce = m_MaxLaunchForce;
				UltFire ();


			} else if (Input.GetButtonDown (m_UltButton)) {
				//have we pressed fired for the first time?
				m_UltFired = false;
				m_CurrentLaunchForce = m_MinLaunchForce;
				//m_ShootingAudio.clip = m_ChargingClip;
				//m_ShootingAudio.Play ();

			} else if ((Input.GetButton (m_UltButton)) && !m_UltFired) {
				// Holding the fire button, not yet fired
				m_CurrentLaunchForce += m_ChargeSpeed*Time.deltaTime;
				m_AimSlider.value = m_CurrentLaunchForce;
				//			anim.Play ("demo_combat_shoot");
			} else if ((Input.GetButtonUp (m_UltButton) && !m_UltFired)) {
				// released button
				//			if (Input.GetKeyUp (m_UltButton)) {
				//				UltFire ();
				//			} 
				UltFire ();
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
		print ("Ult being fired");
		// Instantiate and launch the shell.
		m_UltFired = true;
		print (m_FireTransform.position);
		Transform m_UltFireTransform = m_FireTransform;
		m_UltFireTransform.position.Set (m_UltFireTransform.position.x + 10f, m_UltFireTransform.position.y+4f, m_UltFireTransform.position.z+4f);
		Rigidbody rocketInstance = Instantiate (m_Rocket, m_UltFireTransform.position, m_UltFireTransform.rotation) as Rigidbody;

		rocketInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;
		rocketInstance.gameObject.name = "shell" + m_PlayerNumber;

//		m_ShootingAudio.clip = m_FireClip;
//		m_ShootingAudio.Play ();
		m_CurrentLaunchForce = m_MinLaunchForce;

		m_UltUsed = true;



	}
}