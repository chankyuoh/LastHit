using System;
using UnityEngine;

[Serializable]
public class SoldierManager
{
    public Color m_PlayerColor;            
    public Transform m_SpawnPoint;         
    [HideInInspector] public int m_PlayerNumber;             
    [HideInInspector] public string m_ColoredPlayerText;
    [HideInInspector] public GameObject m_Instance;          
    [HideInInspector] public int m_Wins;                     

    private SoldierMovement m_Movement;       
    private SoldierShooting m_Shooting;
	private SoldierAnimation m_Animation;
	private SoldierHealth m_Health;
    private GameObject m_CanvasGameObject;


    public void Setup()
    {

        m_Movement = m_Instance.GetComponent<SoldierMovement>();
        m_Shooting = m_Instance.GetComponent<SoldierShooting>();
		m_Animation = m_Instance.GetComponent<SoldierAnimation>();
		m_Health = m_Instance.GetComponent<SoldierHealth>();

        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;

        m_Movement.m_PlayerNumber = m_PlayerNumber;
        m_Shooting.m_PlayerNumber = m_PlayerNumber;
		m_Animation.m_PlayerNumber = m_PlayerNumber;
		m_Health.m_PlayerNumber = m_PlayerNumber;

        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";

        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();


        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }
    }


    public void DisableControl()
    {
        m_Movement.enabled = false;
        m_Shooting.enabled = false;
		m_Animation.enabled = false;
        m_CanvasGameObject.SetActive(false);
    }


    public void EnableControl()
    {
        m_Movement.enabled = true;
        m_Shooting.enabled = true;
		m_Animation.enabled = true;

        m_CanvasGameObject.SetActive(true);
    }


    public void Reset()
    {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

		m_Shooting.m_UltUsed = false;
		SoldierHealth.m_didLastHit1 = false;
		SoldierHealth.m_didLastHit2 = false;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }

	public int getRoundWinner() {
		if (m_PlayerNumber == 1 && m_Health.getDidLastHit1 ()) {
			return 1;
		} else if (m_PlayerNumber == 2 && m_Health.getDidLastHit2 ()) {
			return 2;
		} 
		else 
		{
			return -1;

		}
	}

}
