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
 * What actions involve it
 * Query the items inside by placing controller inside, gripping, and looking at items displayed in horizontal (linked to head), list of items
 * Take an item by placing controller inside, gripping and selecting an Item by using the touch pad
 * Place an Item inside by placing the controller inside and releasing the grip (or pressing grip again) (or dropping the item over/near the container)
 */

public class Container : MonoBehaviour
{
  public GameObject m_Camera; //What to display to
  public GameObject m_Container; //What to display from (typicaly self, but for things placed close to the body it might be a hand)

  public float m_DisplayWidth = 1;
  public float m_DisplayHeight = .5f;
  public float m_DisplayItemCount = 3;

  int m_ScrollIDX = 0;
  List<GameObject> m_Contents;

  bool m_DisplayingContentList = false;

  public void AddItem(GameObject obj)
  {
    foreach (MonoBehaviour x in obj.GetComponents<MonoBehaviour>())
      x.enabled = false;
    obj.SetActive(false);
    obj.transform.position = this.transform.position;
    m_Contents.Add(obj);
  }

  public void RemoveItem(GameObject obj)
  {
    foreach (MonoBehaviour x in obj.GetComponents<MonoBehaviour>())
      x.enabled = true;
    obj.transform.position = this.transform.position;
    obj.SetActive(true);
    m_Contents.Remove(obj);
  }

  public GameObject GetHighlightedObj()
  {
    if (m_Contents.Count <= 0) return null;
    if (!m_Contents[m_ScrollIDX]) return null;
    return m_Contents[m_ScrollIDX];
  }

  public void ShowList( bool Show)
  {
    m_DisplayingContentList = Show;
  }

  public void ScrollContentList(int ScrollAmount)
  {
    m_ScrollIDX = Mathf.Clamp(m_ScrollIDX + ScrollAmount, 0, m_Contents.Count - 1);
  }

  public void Update()
  {
    if (m_DisplayingContentList)
      DisplayContentList();
  }

  private void DisplayContentList()
  {

  }
}
