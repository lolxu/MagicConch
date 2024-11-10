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
    
    void Update()
    {
        float loudness = m_detector.GetLoudnessFromMicrophone() * m_loudnessSensibility;

        if (loudness < m_threshold)
        {
            loudness = 0.0f;
        }

        transform.localScale = Vector3.Lerp(m_minScale, m_maxScale, loudness);
    }
}
