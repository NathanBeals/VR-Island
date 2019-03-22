using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Make a big test for inputs I guess
public class PlayerController : MonoBehaviour
{
  public GameObject m_Face;
  public float m_WalkSpeed = 3;
  public float m_RunSpeed = 6;

  float m_Forward = 0, m_Up = 0, m_Right = 0;
  float m_Threshold = .05f;

  void FixedUpdate()
  {
    //Player movement
    if (Mathf.Abs(Input.GetAxis("L Trackpad Horizontal")) > m_Threshold)
      m_Right = Input.GetAxis("L Trackpad Horizontal");

    if (Mathf.Abs(Input.GetAxis("L Trackpad Vertical")) > m_Threshold)
      m_Forward = -Input.GetAxis("L Trackpad Vertical");

    //Clamp so max speed in any direction is 1
    if (m_Forward + m_Right > 1)
    {
      var reduction = m_Forward + m_Right;
      m_Forward /= reduction;
      m_Right /= reduction;
    }

    m_Forward = m_Forward * Time.deltaTime;
    m_Right = m_Right * Time.deltaTime;

    var forwardVector = new Vector3(m_Face.transform.forward.x, 0, m_Face.transform.forward.z).normalized * m_Forward;
    var rightVector = new Vector3(m_Face.transform.right.x, 0, m_Face.transform.right.z).normalized * m_Right;

    transform.position = transform.position + (forwardVector + rightVector) * (Input.GetAxis("L Grip Squeeze") > 0f ? m_RunSpeed : m_WalkSpeed);
  }
}
