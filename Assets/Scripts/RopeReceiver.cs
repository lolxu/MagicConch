using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeReceiver : MonoBehaviour
{
    public List<GameObject> m_links = new List<GameObject>();
    public int m_length = 15;
    public Vector2 m_anchor;
    public GameObject m_ropeEnd;

    public void ConnectToRope(Rigidbody2D ropeEnd)
    {
        HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = ropeEnd;
        joint.anchor = m_anchor;
        joint.connectedAnchor = Vector2.zero;

        GetComponent<Rigidbody2D>().totalForce = Vector2.zero;
    }

    
}
