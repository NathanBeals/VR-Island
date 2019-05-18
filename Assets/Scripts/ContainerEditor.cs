using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Container))]
public class ContainerEditor : Editor
{
  public override void OnInspectorGUI()
  {
    Container myTarget = (Container)target;

    DrawDefaultInspector();

    if (GUILayout.Button("DisplayItems"))
      myTarget.ShowList(true);

    if (GUILayout.Button("HideItems"))
      myTarget.ShowList(false);

    if (GUILayout.Button("AddSomeBullshit"))
      myTarget.AddItem(new StorablePrefab { m_AdditionalDate = "", m_PrefabName = "Floating Light Stone" });

    if (GUILayout.Button("ClearContents"))
      myTarget.ClearContents();
  }
}