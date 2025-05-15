using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RopeGenerator : MonoBehaviour
{
    public Rigidbody2D m_anchorObject;
    
    public GameObject m_usingRopePrefab;
    public GameObject m_connectTo;
    public GameObject m_prev;
    public LineRenderer m_ropeRenderer;

    private int m_ropeLength = 0;

    private void Start()
    {
        var rc = m_connectTo.GetComponent<RopeReceiver>();
        if (m_connectTo != null && rc != null)
        {
            m_ropeLength = rc.m_length;
            m_ropeRenderer.positionCount = m_ropeLength + 2;
        }
        
        GenerateRope(m_connectTo);
    }

    public void GenerateRope(GameObject connectTo)
    {
        CreateVisualRope(connectTo);
    }

    private void CreateVisualRope(GameObject connectTo)
    {
        Rigidbody2D prevRB = m_anchorObject;
        var receiver = connectTo.GetComponent<RopeReceiver>();
        if (receiver != null)
        {
            
            for (int i = 0; i < m_ropeLength; i++)
            {
                // Instantiating a rope link and randomizing scale
                GameObject link = Instantiate(m_usingRopePrefab, null, true);
                link.transform.position = transform.position;
            
                HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
                
                joint.connectedBody = prevRB;
                joint.autoConfigureConnectedAnchor = true;
            
                // Determining the connection offset
                // Making it randomized in the middle
                if (i == 0)
                {
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = Vector2.zero;
                }
                
                if (i == m_ropeLength - 1)
                {
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = Vector2.zero;
                    var connector = connectTo.GetComponent<RopeReceiver>();
                    if (connector != null)
                    {
                        connector.ConnectToRope(link.GetComponent<Rigidbody2D>());
                    }
                }
                else
                {
                    prevRB = link.GetComponent<Rigidbody2D>();
                }
            
                connectTo.GetComponent<RopeReceiver>().m_links.Add(link);
            }
        }
        else
        {
            Debug.LogError("Connecting component does NOT have Rope Receiver");
        }
    }

    private void LateUpdate()
    {
        if (m_connectTo != null)
        {
            RopeReceiver rc = m_connectTo.GetComponent<RopeReceiver>();

            if (rc != null && rc.m_links.Count == m_ropeLength)
            {
                m_ropeRenderer.SetPosition(0, transform.position);
                for (int i = 1; i <= m_ropeLength; i++)
                {
                    Vector3 linkPos = rc.m_links[i-1].transform.position;
                    m_ropeRenderer.SetPosition(i, linkPos);
                }
                m_ropeRenderer.SetPosition(m_ropeLength + 1, m_connectTo.GetComponent<RopeReceiver>().m_ropeEnd.transform.position);
            }
        }
    }
}
