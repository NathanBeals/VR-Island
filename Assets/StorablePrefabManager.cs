using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hack: Lazy/possibly correct way to avoid putting every prefab inside resource folder and loading from there.
//This instead allows for using the in scene linking to store prefabs... (I.E. I can just put prefabs I want to access by name in here)
public class StorablePrefabManager : MonoBehaviour
{
  private static StorablePrefabManager m_Manager;

  public static StorablePrefabManager GetMgr()
  {
    m_Manager = (StorablePrefabManager)FindObjectOfType(typeof(StorablePrefabManager));
    if (m_Manager == null)
      m_Manager = new StorablePrefabManager();

    return m_Manager;
  }

  public List<GameObject> m_Prefabs;

  public GameObject GetPrefab(string Name)
  {
    foreach (var x in m_Prefabs)
      if (x.name == Name) return x;

    return null;
  }
}
