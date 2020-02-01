using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManagement : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Loading Scene");
    }

    public void Load(int sceneNumber)
    {
        Debug.Log("sceneBuildIndex to load: " + sceneNumber);
        SceneManager.LoadScene(sceneNumber);
    }
}