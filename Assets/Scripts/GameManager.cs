using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    
    public UnityEvent StartDialogue = new UnityEvent();
    public UnityEvent EndDialogue = new UnityEvent();

    public List<DialogueScriptable> m_dialogues = new List<DialogueScriptable>();

    private int m_dialogueCounter = 0;
    public int m_tier = 0;
    public int m_firstThreshold = 5;
    public int m_SecondThreshold = 10;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } 
        else 
        {
            _instance = this;
            
            StartDialogue.AddListener(OnStartDialogue);
            EndDialogue.AddListener(OnEndDialogue);
        }
    }

    private void OnDisable()
    {
        StartDialogue.RemoveListener(OnStartDialogue);
        EndDialogue.RemoveListener(OnEndDialogue);
    }

    private void OnEndDialogue()
    {
        m_dialogueCounter++;
        if (m_dialogueCounter > m_firstThreshold || m_dialogueCounter > m_SecondThreshold)
        {
            m_tier++;
            if (m_tier >= m_dialogues.Count)
            {
                m_tier = m_dialogues.Count - 1;
            }
        }
    }

    private void OnStartDialogue()
    {
        
    }

    public string GetRandomDialogue()
    {
        int randomTier = Random.Range(0, m_tier + 1);
        DialogueScriptable currentDialogues = m_dialogues[randomTier];
        Debug.Log(currentDialogues);
        int index = Random.Range(0, currentDialogues.m_dialogues.Count);
        string message = currentDialogues.m_dialogues[index];
        return message;
    }
}
