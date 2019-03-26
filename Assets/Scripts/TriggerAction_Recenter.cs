using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class TriggerAction_Recenter : TriggerAction
{
  public GameObject m_ScriptContainer;
  private Recenter m_Script;

  public override void OnAction()
  {
    base.OnAction();

    if (!m_ScriptContainer) return;

    m_Script = m_ScriptContainer.GetComponent<Recenter>();
    if (!m_Script) return;

    m_Script.ReCenter();
  }
}
