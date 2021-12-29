using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private static bool isExplicit = false;

    void LoadTutorial()
    {
        if(!isExplicit) {
            SceneManager.LoadScene("TestFolder/Implicit Tutorial/Real Implicit Tutorial");
        }else {
            SceneManager.LoadScene("TestFolder/Implicit Tutorial/Real Explicit Tutorial");
        }
    }

    void LoadIntro()
    {
        SceneManager.LoadScene("TestFolder/Implicit Tutorial/Intro");
    }

    void LoadGame()
    {
        SceneManager.LoadScene("TestFolder/Implicit Tutorial/Implicit Tutorial");
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("TestFolder/Implicit Tutorial/Menu");
    }

    void LoadOutro()
    {
        SceneManager.LoadScene("TestFolder/Implicit Tutorial/Outro");
    }
}
