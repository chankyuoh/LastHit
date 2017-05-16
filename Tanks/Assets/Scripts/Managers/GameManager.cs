﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_NumRoundsToWin = 5;        
    public float m_StartDelay = 1f;         
    public float m_EndDelay = 3f;           
    public CameraControl m_CameraControl;   
    public Text m_MessageText;              
    public GameObject m_SoldierPrefab;
	public GameObject m_BigTankPrefab;
	public BigTankManager[] m_BigTank;
    public SoldierManager[] m_Soldiers;           


    private int m_RoundNumber;              
    private WaitForSeconds m_StartWait;     
    private WaitForSeconds m_EndWait;       
    private SoldierManager m_RoundWinner;
    private SoldierManager m_GameWinner;       


    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        SpawnAllSoldiers();
		spawnBigTank ();
        SetCameraTargets();

        StartCoroutine(GameLoop());
    }


    private void SpawnAllSoldiers()
    {
		print (m_Soldiers.Length);
        for (int i = 0; i < m_Soldiers.Length; i++)
        {
			m_Soldiers[i].m_Instance = Instantiate(m_SoldierPrefab, m_Soldiers[i].m_SpawnPoint.position, m_Soldiers[i].m_SpawnPoint.rotation) as GameObject;
            m_Soldiers[i].m_PlayerNumber = i + 1;
            m_Soldiers[i].Setup();
        }
    }

	private void spawnBigTank()
	{
		for (int i = 0; i < m_BigTank.Length; i++){
			m_BigTank[i].m_Instance = Instantiate(m_BigTankPrefab, m_BigTank[i].m_SpawnPoint.position, m_BigTank[i].m_SpawnPoint.rotation) as GameObject;
//			print (m_BigTank.m_Instance);
			m_BigTank[i].Setup ();
		}
	}



    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[m_Soldiers.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = m_Soldiers[i].m_Instance.transform;
        }

        m_CameraControl.m_Targets = targets;
    }


    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (m_GameWinner != null)
        {
			SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting()
    {
		ResetAllTanks ();
		DisableTankControl ();
		m_CameraControl.SetStartPositionAndSize ();
		m_RoundNumber++;
		m_MessageText.text = "ROUND " + m_RoundNumber;
        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
		EnableTankControl ();
		m_MessageText.text = string.Empty;
		while (BigTankLeft()) 
		{
			yield return null;
		}
        
    }


    private IEnumerator RoundEnding()
    {
		DisableTankControl ();
		m_RoundWinner = null;
		m_RoundWinner = GetRoundWinner ();
		if (m_RoundWinner != null) {
			m_RoundWinner.m_Wins++;
		}
		m_GameWinner = GetGameWinner ();

		string message = EndMessage ();
		m_MessageText.text = message;
        yield return m_EndWait;
    }


    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Soldiers.Length; i++)
        {
            if (m_Soldiers[i].m_Instance.activeSelf)
                numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }

	private bool BigTankLeft()
	{
		return m_BigTank [0].m_Instance.activeSelf;
	}



    private SoldierManager GetRoundWinner()
    {
        for (int i = 0; i < m_Soldiers.Length; i++)
        {
			print (m_Soldiers [i].isRoundWinner ());
			print ("eol");
			if (m_Soldiers[i].isRoundWinner()) {
				print ("WOOHOO I WON IM TANK" + i);
				return m_Soldiers [i];
			}
        }

        return null;
    }


    private SoldierManager GetGameWinner()
    {
        for (int i = 0; i < m_Soldiers.Length; i++)
        {
            if (m_Soldiers[i].m_Wins == m_NumRoundsToWin)
                return m_Soldiers[i];
        }

        return null;
    }


    private string EndMessage()
    {
        string message = "DRAW!";

        if (m_RoundWinner != null)
            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < m_Soldiers.Length; i++)
        {
            message += m_Soldiers[i].m_ColoredPlayerText + ": " + m_Soldiers[i].m_Wins + " WINS\n";
        }

        if (m_GameWinner != null)
            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

        return message;
    }


    private void ResetAllTanks()
    {
        for (int i = 0; i < m_Soldiers.Length; i++)
        {
            m_Soldiers[i].Reset();
        }
		m_BigTank [0].Reset ();
    }


    private void EnableTankControl()
    {
        for (int i = 0; i < m_Soldiers.Length; i++)
        {
            m_Soldiers[i].EnableControl();
        }
    }


    private void DisableTankControl()
    {
        for (int i = 0; i < m_Soldiers.Length; i++)
        {
            m_Soldiers[i].DisableControl();
        }
    }
}