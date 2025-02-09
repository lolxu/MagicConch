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

    private void Start()
    {
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
            var length = receiver.m_length;
            for (int i = 0; i < length; i++)
            {
                // Instantiating a rope link and randomizing scale
                GameObject link = Instantiate(m_usingRopePrefab, null, true);
                link.transform.position = transform.position;
                float randScale = Random.Range(0.5f, 0.5f);
                link.transform.localScale = new Vector3(randScale, randScale, randScale);
            
                HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
                
                joint.connectedBody = prevRB;
                joint.autoConfigureConnectedAnchor = false;
            
                // Determining the connection offset
                // Making it randomized in the middle
                if (i == 0)
                {
                    joint.connectedAnchor = Vector2.zero;
                }
                
                if (i == length - 1)
                {
                    var connector = connectTo.GetComponent<RopeReceiver>();
                    if (connector != null)
                    {
                        connector.ConnectToRope(link.GetComponent<Rigidbody2D>());
                    }
                }
                else
                {
                    joint.connectedAnchor = new Vector2(0.0f, -0.25f);
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
    
}
