using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A Storable object is one that can be placed or removed from a container, this could be a transfer between containers or from container to container
public interface IStorable
{
  void Store(); //Stores storable in container, scripts should turn themselves off (soft off), renderers should be toggled off, etc
  void Remove(); //Removes storable from a container, scripts should re-enable themselves (soft on), renderers should be toggled back on, etc.
  string GetPrefabName(); //Get name of objects prefab (saved objects must be instanciatable prefabs)
}

//Base for an object that needs to be placed inside a container
//Objects placed into containers are required to be prefabs for saving and loading purposes. (It's sad I know)
//When an object is placed into a container it is destroyed and only the prefab is stored and instantiated on removal.
//If additional persistant traits are desired, this class can be derived from to store additional information, and the prefab itself may have a script to modify a newly instantiated version using that information
public class StorableGameObject : MonoBehaviour, IStorable
{
  public string m_PrefabName = "Default";
  bool m_Stored = false;

  Dictionary<string, bool> m_EntryValues = new Dictionary<string, bool>
    {
      {"Renderer", true },
      {"Collider", true },
      {"Rigidbody", true }
    };

  public virtual void Start()
  {
    m_EntryValues["Renderer"] = GetComponent<Renderer>().enabled;
    m_EntryValues["Collider"] = GetComponent<Collider>().enabled;
    m_EntryValues["Rigidbody"] = GetComponent<Rigidbody>().isKinematic;

    if (m_Stored)
      Store();
  }

  public virtual void Store()
  {
    Destroy(gameObject);
  }

  public virtual void Remove()
  {

  }

  public virtual void GetPrefabName()
  {

  }
}
