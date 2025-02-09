using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CSVReader))]
public class CSVReaderEditor : Editor
{
    private TextAsset m_myCSV;
    private List<string> m_list = new List<string>();
    
    public override void OnInspectorGUI()
    {
        GUILayout.Label("Conch CSV");
        m_myCSV = EditorGUILayout.ObjectField(m_myCSV, typeof(TextAsset), false) as TextAsset;
        GUILayout.Label("Read CSV");
        if (GUILayout.Button("Read"))
        {
            ReadCSV();
        }
        
        GUILayout.Label("Create the Scriptable Object");
        if (GUILayout.Button("Create"))
        {
            CreateScriptableObject();
        }
    }

    private void ReadCSV()
    {
        var lines = m_myCSV.text.Split('\n');
        
        var lists = new List<List<string>>();
        var columns = 0;
        for (int i = 0; i < lines.Length; i++) 
        {
            var data = lines[i].Split(',');
            var list = new List<string>(data); // turn this into a list
            lists.Add(list); // add this list into a big list
            columns = Mathf.Max(columns, list.Count); // this way we can tell what's the max number of columns in data
        }
        
        for (int col = 0; col < columns; col++) 
        {
            try 
            {
                // we transpose them intentionally
                m_list.Add(lists[0][col]); 
                Debug.Log(lists[0][col]);
            } catch 
            { 
                // with try/catch it won't explode if this particular column/row is out of range
                m_list.Add("*");
            }
        }
    }

    private void CreateScriptableObject()
    {
        DialogueScriptable asset = ScriptableObject.CreateInstance<DialogueScriptable>();
        
        string name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects/"+m_myCSV.name+".asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;

        foreach (var dialogue in m_list)
        {
            asset.m_dialogues.Add(dialogue);
        }
    }
}
