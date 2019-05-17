using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiSceneManager : MonoBehaviour
{
  public List<string> m_Scenes = new List<string>();
  public bool m_ScenesLoading = false;

  private List<AsyncOperation> m_AsncLoads = new List<AsyncOperation>();

  public void OnEnable()
  {
    Load();
  }

  public void LoadScene(string Name)
  {
    if (m_Scenes.Contains(Name)) return; //Scene already loaded

    m_Scenes.Add(Name);
    Load();
  }

  public void UnloadScene(string Name)
  {
    if (!m_Scenes.Contains(Name)) return;
    m_Scenes.Remove(Name);
    SceneManager.UnloadSceneAsync(Name);
  }

  private void Load()
  {
    var unloadedScenes = new List<string>();
    foreach (var n in m_Scenes)
      if (!SceneManager.GetSceneByName(n).IsValid())
        unloadedScenes.Add(n);

    foreach (var n in unloadedScenes)
      m_AsncLoads.Add(SceneManager.LoadSceneAsync(n, LoadSceneMode.Additive));

    StartCoroutine(ScenesLoading());
  }

  private IEnumerator ScenesLoading()
  {
    m_ScenesLoading = true;
    m_AsncLoads.ForEach(s => s.allowSceneActivation = false);

    while (m_AsncLoads.Count > 0)
    {
      bool bScenesReadyToActivate = true;
      foreach (var s in m_AsncLoads) bScenesReadyToActivate &= s.progress >= .9f;
      if (bScenesReadyToActivate)
        m_AsncLoads.ForEach(s => s.allowSceneActivation = true);

      m_AsncLoads.RemoveAll(s => s.progress == 1.0f);
      yield return new WaitForSeconds(.1f);
    }

    m_ScenesLoading = false;
    yield return null;
  }
}
