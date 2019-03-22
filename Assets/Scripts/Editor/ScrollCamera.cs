//Script made by Nathan Beals
//If you have any questions contact bealsn@knights.ucf.edu, Tagline "Unity Scroll Camera"

//Unity does not allow you to change the editor camera's fly speed easily,
// which makes it dificult to work on things that don't fit into the notmal camera fly acceleration curve, 
// especially very small objects under the acceleration min threshold.
//This window, when open, allows one to control the editor camera's fly speed by using the scroll wheel.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScrollCamera : EditorWindow
{
  static float m_CameraSpeed = 1.0f;
  static float m_CameraSpeedFactor = 1.0f / 4.0f;
  static bool m_WindowOpen = false;

  [MenuItem("Utilities/Scroll Camera %q", false, 900)]
  public static void ShowWindow()
  {
    if (!m_WindowOpen)
    {
      m_CameraSpeed = 1.0f;
      GetWindow<ScrollCamera>("Scroll Camera");
      m_WindowOpen = true;
    }
    else
      m_WindowOpen = false;
  }

  //These keys will use the keydown events
  //This prevents a number of unity shortcuts bound to these keys
  List<KeyCode> m_ExceptionalKeyCodes = new List<KeyCode>()
  {
    KeyCode.W,
    KeyCode.S,
    KeyCode.A,
    KeyCode.D,
    KeyCode.Q,
    KeyCode.E,
    KeyCode.F
  };

  void OnSceneGUI(SceneView sceneView)
  {
    if (!m_WindowOpen)
    {
      Close();
      return;
    }

    float x = 0;
    float y = 0;
    float z = 0;

    Event e = Event.current;
    switch (e.type)
    {
      case EventType.KeyDown:
        {
          if (Event.current.keyCode == (KeyCode.W))
            z += m_CameraSpeed;
          if (Event.current.keyCode == (KeyCode.S))
            z -= m_CameraSpeed;
          if (Event.current.keyCode == (KeyCode.A))
            x -= m_CameraSpeed;
          if (Event.current.keyCode == (KeyCode.D))
            x += m_CameraSpeed;
          if (Event.current.keyCode == (KeyCode.Q) && Event.current.modifiers != EventModifiers.Control)
            y -= m_CameraSpeed;
          if (Event.current.keyCode == (KeyCode.E))
            y += m_CameraSpeed;
          if (Event.current.keyCode == (KeyCode.F))
          {
            if (Event.current.modifiers != EventModifiers.Shift)
              MoveToSelection();
            else
              MoveToSelectionAndLookForward();
          }
        }
        break;

      case EventType.ScrollWheel:
        if (Event.current.delta.y != 0)
          m_CameraSpeed = Mathf.Clamp(m_CameraSpeed + (Event.current.delta.y < 0 ? m_CameraSpeed * m_CameraSpeedFactor : -m_CameraSpeed * m_CameraSpeedFactor), 0, 1000);
        break;
    }

    if (Event.current.type == EventType.KeyDown)
    {
      if (m_ExceptionalKeyCodes.Contains(Event.current.keyCode) && !(Event.current.keyCode == KeyCode.Q && Event.current.modifiers == EventModifiers.Control))
        e.Use();
    }
    else if (Event.current.isScrollWheel && Event.current.delta.y != 0)
      e.Use();

    if (x != 0 || y != 0 || z != 0)
      MoveCamera(new Vector3(x, y, z));
  }

  //Moves the camera by a Vector3 not to a Vector3
  void MoveCamera(Vector3 Movement)
  {
    Vector3 position = SceneView.lastActiveSceneView.pivot;

    var camTran = Camera.current.transform;
    var x = camTran.right * Movement.x;
    var y = camTran.up * Movement.y;
    var z = camTran.forward * Movement.z;

    position += x;
    position += y;
    position += z;

    SceneView.lastActiveSceneView.pivot = position;
    SceneView.lastActiveSceneView.size = 0;
    SceneView.lastActiveSceneView.Repaint();
  }

  static void MoveToSelection()
  {
    if (Selection.gameObjects.Length < 0) return;

    //Moves the camera to the gameobject
    SceneView.lastActiveSceneView.pivot = Selection.gameObjects[0].transform.position;
    SceneView.lastActiveSceneView.size = 0;
    SceneView.lastActiveSceneView.Repaint();
  }

  //Used the selected objects forward
  static void MoveToSelectionAndLookForward()
  {
    if (Selection.gameObjects.Length < 0) return;

    var targetTransform = Selection.gameObjects[0].transform;
    var newdir = Quaternion.LookRotation(targetTransform.forward, targetTransform.up);
    SceneView.lastActiveSceneView.LookAt(targetTransform.position, newdir, 0);
    SceneView.lastActiveSceneView.Repaint();
  }

  private void OnGUI()
  {
   GUILayout.TextArea(
     "This camera overrides some functionality of the base editor controls, \n" +
     "while this window is open some controls may not behave as normal. \n" +
     "------ \n" +
     "Ctrl+Q -> Toggles Window \n" +
     "F -> Focus on Gameobject \n" +
     "F+Shft -> Focus on Gameobject + Look in Direction of Gameobjects Forward Vector \n" +
     "ScrollWheel+/- -> Increase/Decrease the Camera Speed (CS +/-(CS*SF)) \n" +
     "QWEASD -> Move the Camera, Hold Right Mouse to fly \n" +
     "I'm sorry the farviewplane is clamped to 1000, I'm trying to fix it \n If you know how please tell me.");

    m_CameraSpeed = EditorGUILayout.FloatField("Camera Speed", m_CameraSpeed);
    m_CameraSpeedFactor = EditorGUILayout.FloatField("Speed Factor", m_CameraSpeedFactor);
  }

  private void OnDestroy()
  {
    m_WindowOpen = false;
  }

  private void OnEnable()
  {
    SceneView.onSceneGUIDelegate += OnSceneGUI;
  }

  private void OnDisable()
  {
    SceneView.onSceneGUIDelegate -= OnSceneGUI;
  }
}