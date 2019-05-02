using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Activates physics only once loading is complete
public class WaitOnLoad : MonoBehaviour
{
  public Rigidbody m_Target;
  public float m_Delay = 1.0f;

  public void Start()
  {
    EnablePhysics(false);
    StartCoroutine(EnableOnLoad());
  }

  private IEnumerator EnableOnLoad()
  {
    var msm = GameObject.FindObjectOfType<MultiSceneManager>(); //TODO: convert Multiscenemanager to singleton
    while (msm.m_ScenesLoading)
      yield return new WaitForSeconds(.1f);

    yield return new WaitForSeconds(m_Delay);
    EnablePhysics(true);
  }

  public void EnablePhysics(bool Active)
  {
    //Rigidbody.sleep //Does not do what you think it does
    m_Target.isKinematic = !Active;
    m_Target.useGravity = Active;
  }
}
