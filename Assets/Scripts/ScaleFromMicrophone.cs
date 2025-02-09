using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFromMicrophone : MonoBehaviour
{
    public AudioSource m_source;
    public Vector3 m_minScale;
    public Vector3 m_maxScale;
    public AudioLoudness m_detector;

    public float m_loudnessSensibility = 100.0f;
    public float m_threshold = 0.1f;

    private bool m_canScale = false;

    private void Start()
    {
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
        m_canScale = false;
    }

    private void OnStartDialogue()
    {
        m_canScale = true;
    }

    private void Update()
    {
        if (m_canScale)
        {
            float loudness = m_detector.GetLoudnessFromMicrophone() * m_loudnessSensibility;

            if (loudness < m_threshold)
            {
                loudness = 0.0f;
            }

            transform.localScale = Vector3.Lerp(m_minScale, m_maxScale, loudness);
        }
        else
        {
            transform.localScale = m_minScale;
        }
    }
}
