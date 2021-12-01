using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTutorial : MonoBehaviour
{
    public ResultDisplayBehavior resultDisplay;

    void GetResults()
    {
        resultDisplay.ClearTutorial();
    }

    void Quit()
    {
        SceneManager.LoadScene("TestFolder/Implicit Tutorial/Intro 2");
    }
}
