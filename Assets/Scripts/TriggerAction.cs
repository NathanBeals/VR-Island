using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class TriggerAction : MonoBehaviour
{
  public Valve.VR.SteamVR_Action_Boolean m_Action;
  public SteamVR_Input_Sources m_InputSource;

  public void OnEnable()
  {
    if (m_Action != null)
      m_Action.AddOnChangeListener(OnAction, m_InputSource);
  }

  public void OnDisable()
  {
    if (m_Action != null)
      m_Action.RemoveOnChangeListener(OnAction, m_InputSource);
  }

  public void OnAction(Valve.VR.SteamVR_Action_Boolean ActionIn, Valve.VR.SteamVR_Input_Sources InputSource, bool state)
  {
    if (!state) return;
    OnAction();
  }

  public virtual void OnAction()
  {

  }
}
