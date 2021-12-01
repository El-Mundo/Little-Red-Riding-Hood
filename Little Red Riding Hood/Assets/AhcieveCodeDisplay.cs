using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AhcieveCodeDisplay : MonoBehaviour
{
    private Animator animator;
    public InputField display;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TurnOn()
    {
        display.text = 
            PlayerPrefs.GetInt("Highest Score", 0) + "-" + PlayerPrefs.GetInt("Game Time", 0) + "-" + PlayerPrefs.GetInt("Boss Defeated", 0) + ":"
            + PlayerPrefs.GetString("Result 1", "NULL") + ":"
            + PlayerPrefs.GetString("Result 2", "NULL") + ":"
            + PlayerPrefs.GetString("Result 3", "NULL") + ":"
            + PlayerPrefs.GetString("Result 4", "NULL") + ":"
            + PlayerPrefs.GetString("Result 5", "NULL") + ":";
        animator.Play("Achieve Code Enter");
    }

    public void TurnOff()
    {
        animator.Play("Off");
    }
}
