using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Drag : MonoBehaviour
{
    [SerializeField] private float m_clickRadius = 0.5f;

    public AudioClip pullSoundEffect; // added by free Drag your sound file here in the Inspector
    public AudioClip goBackSoundEffect; // added by free Drag your sound file here in the Inspector
    public AudioSource audioSource; // added by free
    private bool pullSoundPlayed = false; // added by free

    private bool m_isGrabbing = false;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!pullSoundPlayed)
            {
                audioSource.PlayOneShot(pullSoundEffect); // only once
                pullSoundPlayed = true;
            }

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.CircleCast(mouseWorldPos, m_clickRadius, Vector2.zero);
            if (hit && hit.collider.CompareTag("Target"))
            {
                mouseWorldPos.z = 0.0f;
                hit.collider.gameObject.transform.position = mouseWorldPos;
                m_isGrabbing = true;
                GameManager.Instance.StartDialogue.Invoke();
            }
            else
            {
                m_isGrabbing = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (m_isGrabbing)
            {
                audioSource.PlayOneShot(goBackSoundEffect); // added by free
                m_isGrabbing = false;
                GameManager.Instance.EndDialogue.Invoke();
            }

            pullSoundPlayed = false; // added by free, reset for the next drag
        }
    }

}
