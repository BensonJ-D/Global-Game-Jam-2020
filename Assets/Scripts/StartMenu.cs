using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Player 1 Right"))
        {
            SceneManager.LoadSceneAsync("CleanMaybe");
        }
    }
}
