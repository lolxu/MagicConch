using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RopeGenerator : MonoBehaviour
{
    public Rigidbody2D m_anchorObject;
    
    [Tooltip("The rope asset that is used to connect to this object")]
    public GameObject m_myRopePrefab;
    
    [Tooltip("The rope asset that is using to connect to other objects")]
    public GameObject m_usingRopePrefab;
    public List<GameObject> m_next = new List<GameObject>();
    public GameObject m_prev;

    private GameObject m_rope;

    private void Start()
    {
        m_rope = GameObject.FindGameObjectWithTag("Rope");
    }

    public void GenerateRope(GameObject connectTo)
    {
        if (connectTo.GetComponent<RelativeJoint2D>() != null)
        {
            Vector2 offset = transform.position - connectTo.transform.position;
            RelativeJoint2D joint = connectTo.GetComponent<RelativeJoint2D>();
            joint.enabled = true;
            joint.connectedBody = m_anchorObject;
            joint.autoConfigureOffset = true;
            // joint.linearOffset = offset;
        }
        
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
                GameObject link = Instantiate(m_usingRopePrefab, m_rope.transform, true);
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
