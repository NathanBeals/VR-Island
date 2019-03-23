using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class Right_Grippy_Script : MonoBehaviour
{
  [SerializeField]
  private Valve.VR.SteamVR_Action_Boolean m_Action;

  public SteamVR_Input_Sources m_InputSource;

  public void OnEnable()
  {
    if (m_Action != null)
      m_Action.AddOnChangeListener(OnRightGrippy, m_InputSource);
  }

  public void OnDisable()
  {
    if (m_Action != null)
      m_Action.RemoveOnChangeListener(OnRightGrippy, m_InputSource);
  }

  public void OnRightGrippy(Valve.VR.SteamVR_Action_Boolean ActionIn, Valve.VR.SteamVR_Input_Sources InputSource, bool whatever)
  {
    if (whatever)
      Debug.Log(m_InputSource.ToString() + " Hit");
  }
}
