using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Consider adding more player attributes
/*
 * Height of average american male, 5'9" (175cm)
 * Average head height, 8-9" (20-22cm), eyes are centeredish so - 4.5" from head top (input height)
 * Is a modifiable wingspan attribute necissary? Would it be like a spell?
 * 
 * 
 * 
 */


public class PlayerTraits : MonoBehaviour
{
  //Units in meters 
  public GameObject Body = null;
  public float Thickness
  {
    set
    {
      m_Thickness = value;
      AdjustBody();
    }
    get { return this.m_Thickness; }
  }
  public float Height 
  {
    set
    {
      m_Height = value;
      m_EyeLevel = m_Height - (10 / 100.0f);
      AdjustBody();
    }
    get { return this.m_Height; }
  }

  public void Start()
  {
    AdjustBody();
  }

  public void AdjustBody()
  {
    if (Body == null) return;
    if (Body.GetComponent<Rigidbody>() == null || Body.GetComponent<CapsuleCollider>() == null) return;

    var CC = Body.GetComponent<CapsuleCollider>();
    CC.height = Mathf.Abs(m_Height);
    CC.center = new Vector3(0, m_Height / 2, 0);
    CC.radius = m_Thickness;
  }

  //Serialized so Undo works, but then hidden in inspector because no
  [HideInInspector]
  [SerializeField]
  private float m_Height = 0;
  [HideInInspector]
  [SerializeField]
  private float m_EyeLevel = 0;
  [HideInInspector]
  [SerializeField]
  private float m_Thickness = 0;

}
