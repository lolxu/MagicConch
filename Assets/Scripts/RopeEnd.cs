using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeEnd : MonoBehaviour
{
    [SerializeField] private GameObject m_conch = null;
    [SerializeField] private float m_rotateSpeed = 180.0f;

    private Rigidbody2D m_RB;

    private void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (m_conch != null)
        {
            Vector3 toTarget = (m_conch.transform.position - transform.position).normalized;
            Quaternion targetQuat = Quaternion.LookRotation(toTarget, Vector3.forward);
            Vector3 tmp = targetQuat.eulerAngles;
            tmp.z += 30.0f;
            targetQuat = Quaternion.Euler(tmp);
            Quaternion rotQuat = Quaternion.Slerp(transform.rotation, targetQuat, Time.deltaTime * m_rotateSpeed);
            m_RB.MoveRotation(rotQuat);
        }
    }
}
