using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    public Animator animator;
    public Text tipText;

    public void Tip(string content)
    {
        tipText.text = content;
        animator.Play("Tip", 1);
    }

    public void PushMemories()
    {
        animator.Play("Switch Menu");
    }

    public void PushBack()
    {
        animator.Play("Switch Menu Back");
    }

    public void PushStart()
    {
        if(PlayerPrefs.GetInt("Tutorial", 0) == 0) {
            Tip("请先完成 回忆-开幕");
        }else {
            animator.Play("Out", 2);
        }
    }

    public void PushIntro()
    {
        animator.Play("Tutorial", 2);
    }

    public void PushOutro()
    {
        if(PlayerPrefs.GetInt("Boss Defeated", 0) > 0) {
            animator.Play("Outro", 2);
        }else {
            Tip("结局可在击败游戏BOSS后解锁");
        }
    }
}
