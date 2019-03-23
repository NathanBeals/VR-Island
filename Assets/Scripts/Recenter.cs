using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recenter : MonoBehaviour
{
  public GameObject m_Body; //Requires Collider
  private CapsuleCollider m_BodyCollider;
  private float m_BodyHeight;
  public GameObject m_Head;
  public GameObject m_CenteringOffset;

  //Get the new position
  //Hide the view
  //Move the body to position


  //Position is the head camera
  //move to the x,z position
  //move the body
  //Calc the y of the body
  //Raycast from the head to the ground
  //put the body at 1/2 * y + ground.y (to reduce shuddering)
  //THEN
  //Tell the CenterOffsetBody that the body has moved by (old to new position) and to move by -(old to new position) to correct the camera position

  //MoveTo the head (VR Camera)
  public void ReCenter()
  {
    MoveToTargetPosition(m_Head.transform.position);
  }

  //Move the body to a new position (x,y from the passed transform, y from the collider height)
  public void MoveToTargetPosition(Vector3 NewPosition)
  {
    if (!m_Body) { Debug.LogError("No Body"); return; }

    m_BodyCollider = m_Body.GetComponent<CapsuleCollider>();
    m_BodyHeight = m_BodyCollider.height;
    
    if (!m_BodyCollider) { Debug.LogError("Body requires capsule collider."); return; }

    RaycastHit hit;
   // Debug.DrawRay(m_Head.transform.position, -Vector3.up * 100);
    if (Physics.Raycast(m_Head.transform.position, -Vector3.up, out hit, 100)) //Use Layer Mask at some point
    {
      var newPos = NewPosition;
      newPos.y = hit.point.y - m_BodyHeight / 2f;

      var oldPos = m_Body.transform.position;

      var moveVec = newPos - oldPos;
      moveVec.y = 0;

      //Move the body
      m_Body.transform.position = newPos;
      //Move the head offset in the opposite direction of the move, to correct the vr hooks
      m_CenteringOffset.transform.position = m_CenteringOffset.transform.position - moveVec;
    }
  }
}
