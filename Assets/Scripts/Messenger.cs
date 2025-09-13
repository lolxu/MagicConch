using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Messenger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private float m_showDuration = 5.0f;    
    
    private void Start()
    {
        m_text.gameObject.SetActive(false);
        
        GameManager.Instance.StartDialogue.AddListener(OnStartDialogue);
        GameManager.Instance.EndDialogue.AddListener(OnEndDialogue);
    }

    private void OnDisable()
    {
        GameManager.Instance.StartDialogue.RemoveListener(OnStartDialogue);
        GameManager.Instance.EndDialogue.RemoveListener(OnEndDialogue);
    }

    private void OnEndDialogue()
    {
        string message = GameManager.Instance.GetRandomDialogue();
        m_text.text = message;
        m_text.gameObject.SetActive(true);
        StartCoroutine(HideMessage());
    }

    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(m_showDuration);
        m_text.gameObject.SetActive(false);
    }

    private void OnStartDialogue()
    {
        StopAllCoroutines();
        m_text.gameObject.SetActive(false);
    }
}
