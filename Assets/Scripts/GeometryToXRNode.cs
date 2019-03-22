using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GeometryToXRNode : MonoBehaviour
{
  public GameObject m_RootBody;
  public XRNode m_Node;

  public void Start()
  {
    RandomizePosition();
  }

  //HACK: I honestly don't care at all where these go as long as it's not on top of eachother (at 0,0,0)
  public void RandomizePosition()
  {
    var offset = -100;
    gameObject.transform.localPosition = new Vector3(Random.Range(1, 100), Random.Range(1, 100), Random.Range(1, 100)) * offset;
  }

  private void FixedUpdate()
  {
    gameObject.transform.localPosition = InputTracking.GetLocalPosition(m_Node);
    gameObject.transform.localRotation = InputTracking.GetLocalRotation(m_Node);

    if (gameObject.transform.localPosition == Vector3.zero)
      RandomizePosition();
  }
}
