using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;         
    public float m_Speed = 9f;            
    public float m_TurnSpeed = 180f;            

    
    private string m_MovementAxisName;     
    private string m_TurnAxisName;         
    private Rigidbody m_Rigidbody;         
    private float m_MovementInputValue;    
    private float m_TurnInputValue;        
    private float m_OriginalPitch;         

	// when scene first starts
    private void Awake()
    {
		// GetComponent = storing reference to specified game object
        m_Rigidbody = GetComponent<Rigidbody>();
    }

	// when they become alive
    private void OnEnable ()
    {
		//kinematic == no forces will be applied to it if true
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }

	// when they die
    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

    }
    

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
		m_MovementInputValue = Input.GetAxis(m_MovementAxisName); // value from -1 to 1
		m_TurnInputValue = Input.GetAxis (m_TurnAxisName);
    }





    private void FixedUpdate()
    {
        // Move and turn the tank.

		Move ();
		Turn ();
    }


    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
		Vector3 movement = transform.forward*m_MovementInputValue* m_Speed * Time.deltaTime;
		m_Rigidbody.MovePosition (m_Rigidbody.position + movement);
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
		float turn = m_TurnInputValue * m_TurnSpeed*Time.deltaTime;
		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
		m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
    }
}