using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultDisplayBehavior : MonoBehaviour
{
    public Text performScore, distanceScore, totalScore, highestDisplay, gameTimeDisplay;
    public Image[] ticks;
    public Button backButton;
    public DEMO_AchievementBehavior achievement;
    public DEMO_ManagerBehavior manager;
    private bool[] achs;
    private bool active = false;
    private int timer = 0;
    public AudioSource tickSound;
    private int highestScore, performNum, distanceNum, gameTime;
    public bool isTutorial = false;
    private bool tutorialClear = false;

    void Start()
    {
        highestScore = PlayerPrefs.GetInt("Highest Score", 0);
    }

    void FixedUpdate()
    {
        if(active && timer < 200) {
            timer ++;
            switch(timer) {
                case 3:
                    backButton.interactable = false;
                    PlayerPrefs.Save();
                    Debug.Log("SAVED");
                    break;
                case 30:
                    CheckTick(0);
                    break;
                case 60:
                    CheckTick(1);
                    break;
                case 90:
                    CheckTick(2);
                    break;
                case 120:
                    CheckTick(3);
                    break;
                case 150:
                    CheckTick(4);
                    break;
                case 180:
                    ShowBackButton();
                    break;
            }
        }
    }

    void ShowBackButton()
    {
        backButton.interactable = true;
    }

    void CheckTick(int index)
    {
        if(achs[index] == true) {
            ticks[index].color = new Color(1, 1, 1, 1);
            tickSound.Play();
        }
    }

    public void PreloadAch()
    {
        if(isTutorial) {
            return;
        }

        achs = achievement.getAchievements();
        foreach(Image img in ticks) {
            img.color = new Color(1, 1, 1, 0);
        }
        performNum = manager.GetPerformScore();
        performScore.text = performNum.ToString();

        distanceNum = manager.GetDistanceScore();
        distanceScore.text = distanceNum.ToString();

        int sc  = manager.GetScore();
        totalScore.text = sc.ToString();
        highestDisplay.text = highestScore.ToString();

        if(highestScore < sc) {
            PlayerPrefs.SetInt("Highest Score", sc);
        }

        gameTime = PlayerPrefs.GetInt("Game Time", 0);
        gameTime ++;
        PlayerPrefs.SetInt("Game Time", gameTime);
        gameTimeDisplay.text = gameTime.ToString();

        //打败BOSS，保存打败的场次
        if(achs[4] == true) {
            if(PlayerPrefs.GetInt("Boss Defeated", 0) == 0) {
                PlayerPrefs.SetInt("Boss Defeated", gameTime);
            }
        }
    }

    string GetResultCode()
    {
        string achBool = "";
        foreach(bool b in achs) {
            if(b == true) {
                achBool += "1";
            }else {
                achBool += "0";
            }
        }
        return distanceNum + "-" + performNum + "-" + achBool;
    }

    void TurnOn()
    {
        if(isTutorial) {
            if(tutorialClear) {
                PlayerPrefs.SetInt("Tutorial", 1);
                PlayerPrefs.Save();
            }else {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        active = true;
        Debug.Log(GetResultCode());

        for(int i = 1; i <= 5; i ++) {
            string ix = "Result " + i;
            if(PlayerPrefs.GetString(ix, "NULL") == "NULL") {
                Debug.Log("SAVE #" + i);
                PlayerPrefs.SetString(ix, GetResultCode());
                break;
            }
        }
    }

    public void PushBackButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TestFolder/Implicit Tutorial/Menu");
    }

    public void ClearTutorial()
    {
        tutorialClear = true;
        manager.gameOver();
        PlayerPrefs.SetInt("Tutorial", 1);
        PlayerPrefs.Save();
        Debug.Log("TUTORIAL CLEAR");
    }
}
