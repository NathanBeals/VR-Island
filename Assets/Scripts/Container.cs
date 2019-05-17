using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * What is it?
 * List of items, contents
 * An displayed object in the world
 * Can be queried for items, items can be taken, or placed inside
 * 
 * What actions involve it?
 * Query the items inside by placing controller inside, gripping, and looking at items displayed in horizontal (linked to head), list of items
 * Take an item by placing controller inside, gripping and selecting an Item by using the touch pad
 * Place an Item inside by placing the controller inside and releasing the grip (or pressing grip again) (or dropping the item over/near the container)
 */

public class Container : MonoBehaviour
{
  public GameObject m_Camera; //What to display to
  public GameObject m_Container; //What to display from (typicaly self, but for things placed close to the body it might be a hand)

  public float m_DistFromCamera = 1.0f;
  public float m_Curvature = 0.0f; //Degree Curvature 0 - 360
  public float m_ItemHDist = .5f;

  public float m_DisplayWidth = .5f;
  public float m_DisplayHeight = .5f;
  public int m_DisplayItemCount = 3;

  int m_ScrollIDX = 0;
  List<StorablePrefab> m_Contents = new List<StorablePrefab>();
  List<GameObject> m_DisplayItems = new List<GameObject>(); //Smaller instantiated contents with disabled scripts

  bool m_DisplayingContentList = false;
  bool m_ListDirty = true;

  public void AddItem(StorablePrefab obj)
  {
    m_Contents.Add(obj);
    m_ListDirty = true;
  }

  public void RemoveItem(StorablePrefab obj)
  {
    m_Contents.Remove(obj);
    m_ListDirty = true;
  }

  public StorablePrefab GetHighlightedObj()
  {
    return m_Contents[m_ScrollIDX];
  }

  public void ShowList(bool Show)
  {
    m_DisplayingContentList = Show;

    foreach (var x in m_DisplayItems)
      x.GetComponent<Renderer>().enabled = Show;
  }

  public void ScrollContentList(int ScrollAmount)
  {
    m_ScrollIDX = Mathf.Clamp(m_ScrollIDX + ScrollAmount, 0, m_Contents.Count - 1);
  }

  //Create a list of smaller objects that ignore collision and physics
  private void UpdateStores()
  {
    foreach (var x in m_DisplayItems)
      Destroy(x);

    foreach (var x in m_Contents)
    {
      m_DisplayItems.Add(Instantiate(StorablePrefabManager.GetMgr().GetPrefab(x.m_PrefabName), m_Camera.transform.position + (m_Camera.transform.forward * m_DistFromCamera), Quaternion.identity));
      var lastElem = m_DisplayItems[m_DisplayItems.Count - 1];

      foreach (MonoBehaviour y in lastElem.GetComponents<MonoBehaviour>())
        y.enabled = true;

      if (lastElem.GetComponent<Collider>())
        lastElem.GetComponent<Collider>().enabled = false;

      if (lastElem.GetComponent<Rigidbody>())
      {
        lastElem.GetComponent<Rigidbody>().isKinematic = false;
        lastElem.GetComponent<Rigidbody>().useGravity = false;
      }
    }

    m_ListDirty = false;
  }
  public void Update()
  {
    if (m_DisplayingContentList)
      UpdateContentDisplay();
  }

  //Runs in update loop
  private void UpdateContentDisplay()
  {
    if (m_ListDirty)
      UpdateStores();

    if (m_DisplayItems.Count < 1) return;

    if (m_ScrollIDX > m_DisplayItems.Count - 1) m_ScrollIDX = 0;
    if (m_ScrollIDX < m_DisplayItems.Count - 1) m_ScrollIDX = m_DisplayItems.Count - 1;

    int i = m_DisplayItemCount;

    //TODO: figure out how to display items in a variable display
    //TODO: WORKING HERE
    for (int x = 0; x < m_DisplayItemCount; x++)
    {

    }
  }
}
