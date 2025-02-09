using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Conch Dialogue", menuName = "ScriptableObjects/Conch Dialogue")]
public class DialogueScriptable : ScriptableObject
{
    public List<string> m_dialogues = new List<string>();
}
