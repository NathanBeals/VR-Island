using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(PlayerTraits))]
public class PlayerTraitsEditor : Editor
{
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    PlayerTraits pt = (PlayerTraits)target;
    UndoField("Height", pt, "Height");
    UndoField("Thickness", pt, "Thickness");
  }

  //TODO: Conver to Generic, Move to helper namespace
  private void UndoField(string Descriptor, PlayerTraits PT, string FieldName)
  {
    Type t = typeof(PlayerTraits);
    foreach (var x in t.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic))
    {
      if (x.Name == FieldName)
      {
        var inputVal = EditorGUILayout.FloatField(Descriptor, (float)x.GetValue(PT));
        if (inputVal != (float)x.GetValue(PT))
        {
          Undo.RecordObject(PT, x.Name + " Change");
          x.SetValue(PT, inputVal);
        }

        break;
      }
    }
  }

}
