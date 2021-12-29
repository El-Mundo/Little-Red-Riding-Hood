using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimeDelate : MonoBehaviour
{
    public GameObject text;
    public int TotalTime = 60;
    public int index=0;

    void Start()
    {
        StartCoroutine(Time());
    }

    IEnumerator Time()
    {
        while (TotalTime >= 0)
        {
            text.GetComponent<Text>().text = TotalTime.ToString();
            yield return new WaitForSeconds(1);
            if (GameMgr.instance.state != GameState.End)
            {
                TotalTime--;
                if (TotalTime <= 0)
                {
                    Debug.Log("game over");

                    //if (index==3)
                    //{
                    //    index = 0;
                    //}
                    SceneManager.LoadScene(Random.Range(0,4));
                    // index++;
                }
            }

        }
    }

}