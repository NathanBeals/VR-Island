using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: in progress

public class InventoryItem
{
  public string ReferenceName = "Default";
  public string DisplayName = "Display Name";
  public string Description = "";
  public GameObject WorldModel = null;
  public GameObject InventoryModel = null;
}

public class Inventory : MonoBehaviour
{
  private List<InventoryItem> m_StoredItems = new List<InventoryItem>();


  public void AddItem(string Name)
  {
    //Use prefab model if no model is provided for the display model
    //If the actual model is 


  }

  public void ReomoveItem()
  {

  }

  public void ClearList()
  {
    m_StoredItems.Clear();
  }
}
