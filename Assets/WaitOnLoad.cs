using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Activates physics only once loading is complete
public class WaitOnLoad : MonoBehaviour
{
  public Rigidbody m_Target;

  public void OnStart()
  {
    m_Target.Sleep();
    StartCoroutine(EnableOnLoad());
  }

  private IEnumerator EnableOnLoad()
  {
    var msm = GameObject.FindObjectOfType<MultiSceneManager>(); //TODO: convert Multiscenemanager to singleton
    while (msm.m_ScenesLoading)
      yield return new WaitForSeconds(.1f);

    m_Target.WakeUp();
  }
}
