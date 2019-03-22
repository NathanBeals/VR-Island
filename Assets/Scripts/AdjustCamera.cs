using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCamera : MonoBehaviour
{
  public GameObject m_Body;

  public Vector3 m_NewFacePosition = Vector3.zero;

  void Update()
  {
    if (!m_Body) return;

    var cameraDif = transform.localPosition - m_NewFacePosition;
    m_Body.transform.position = m_Body.transform.position + (new Vector3(cameraDif.x, cameraDif.y, cameraDif.z)) * transform.localScale.magnitude;

    m_NewFacePosition = transform.localPosition;
  }
}
