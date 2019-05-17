using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Storable prefab only contains a name that can be used to find it, 
//and data (a string for save side simplicity) that can be parsed by PrefabType specific scripts to instantiate a modified prefab
//Expl, a torch has burned out. When instanciating a new prefab, the Torch prefave will be instantiated, then a derivative of this script will set its state to "burned out". 
public class StorablePrefab : MonoBehaviour
{
  public string m_PrefabName = "Default Prefab";
  public string m_AdditionalDate = "Additional Data";

  public virtual void HandleAdditionalStateData(string Additional_Data) { }
}
