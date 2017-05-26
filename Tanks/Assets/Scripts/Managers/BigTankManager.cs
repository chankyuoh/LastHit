using System;
using UnityEngine;

[Serializable]
public class BigTankManager
{
    public Color m_PlayerColor;            
    public Transform m_SpawnPoint;         
    [HideInInspector] public int m_PlayerNumber = 3;             
    [HideInInspector] public string m_ColoredPlayerText;
    [HideInInspector] public GameObject m_Instance;          
    [HideInInspector] public int m_Wins;                     

	private SoldierHealth m_Health;
	private BigTankShooting m_Shooting;
    private GameObject m_CanvasGameObject;


    public void Setup()
    {
		m_Health = m_Instance.GetComponent<SoldierHealth>();
		m_Shooting = m_Instance.GetComponent<BigTankShooting> ();
		m_Shooting.m_PlayerNumber = m_PlayerNumber;

        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;


        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";

        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();


        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }
    }


    public void DisableControl()
    {
		m_Shooting.CancelInvoke ();
		m_Shooting.enabled = false;
        m_CanvasGameObject.SetActive(false);

    }


    public void EnableControl()
    {
		m_Shooting.enabled = true;
        m_CanvasGameObject.SetActive(true);
		m_Shooting.InvokeRepeating ("Fire", 0.0f, 0.3f);

    }


    public void Reset()
    {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }

	public bool isRoundWinner() {
		return SoldierHealth.m_didLastHit1;
	}

}
