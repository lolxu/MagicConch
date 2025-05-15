using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioLoudness : MonoBehaviour
{
    public int m_sampleWindow = 64;

    private AudioClip m_micClip;

    private void Start()
    {
        MicrophoneToAudioClip();
    }

    private void MicrophoneToAudioClip()
    {
        string microphoneName = Microphone.devices[0];
        Debug.Log(microphoneName);
        m_micClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudio(Microphone.GetPosition(Microphone.devices[0]), m_micClip);
    }
    
    public float GetLoudnessFromAudio(int clipPos, AudioClip clip)
    {
        int startPos = clipPos - m_sampleWindow;

        if (startPos < 0)
        {
            return 0.0f;
        }
        
        float[] waveData = new float[m_sampleWindow];
        clip.GetData(waveData, startPos);
        
        // Compute Loudness
        float totalLoudness = 0.0f;
        for (int i = 0; i < waveData.Length; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / waveData.Length;
    }
}
