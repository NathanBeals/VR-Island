using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritModeEffect : MonoBehaviour
{
  public GameObject m_Head;
  public GameObject m_Body;
  public float m_Height = 1.75f;
  public GameObject m_LowEffect;
  public GameObject m_HighEffect;
  public float m_LowEffectDistance = 2f;
  public float m_HighEffectDistance = 3f;

  [SerializeField]
  private float m_CurDist = 0f;

  private void OnEnable()
  {
    m_LowEffect.SetActive(false);
    m_HighEffect.SetActive(false);
  }

  private void OnDisable()
  {
    if (m_LowEffect)
      m_LowEffect.SetActive(false);
    if (m_LowEffect)
      m_HighEffect.SetActive(false);
  }

  public void Update()
  {
    if (!m_Head || !m_Body || !m_LowEffect || !m_HighEffect) return;

    m_LowEffect.transform.position = m_Body.transform.position;
    m_HighEffect.transform.position = m_Body.transform.position;

    m_CurDist = Vector3.Distance(Vector3.Cross(m_Head.transform.position, Vector3.up), Vector3.Cross(m_Body.transform.position, Vector3.up));
    if (m_CurDist > m_HighEffectDistance)
    {
      m_HighEffect.SetActive(true);
      m_LowEffect.SetActive(false);
    }
    else if (m_CurDist > m_LowEffectDistance)
    {
      m_HighEffect.SetActive(false);
      m_LowEffect.SetActive(true);
    }
    else
    {
      m_HighEffect.SetActive(false);
      m_LowEffect.SetActive(false);
    }

  }
}
