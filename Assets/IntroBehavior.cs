using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroBehavior : MonoBehaviour
{
    public string[] lines;
    public int[] changeImage;
    public int sleepTime = 60;
    public Text text;
    private int time, lineNum, imgNum;
    public Animator animator;
    /*public Text tip1, tip2;
    public int tipNum1, tipNum2;*/
    //Color TIP_COLOR = new Color(0.8f, 0.2f, 0.2f, 1.0f);
    //public bool partOne = true;

    void Start()
    {
        time = sleepTime;
        lineNum = 0;
        imgNum = 0;
    }

    void FixedUpdate()
    {
        if(time > 0) {
            time --;
        }else {
            animator.Play("On", 1);
        }
    }

    public void Touch()
    {
        if(time <= 0) {
            time = sleepTime;
            if(lineNum < lines.Length - 1) {
                lineNum ++;
                text.text = lines[lineNum];
                /*if(partOne) {
                    if(lineNum == tipNum1) {
                        tip1.color = TIP_COLOR;
                        tip2.color = new Color(1, 1, 1, 0);
                    }else if(lineNum == tipNum2) {
                        tip2.color = TIP_COLOR;
                        tip1.color = new Color(1, 1, 1, 0);
                    }else {
                        tip1.color = new Color(1, 1, 1, 0);
                        tip2.color = new Color(1, 1, 1, 0);
                    }
                }*/

                foreach(int c in changeImage) {
                    if(lineNum == c) {
                        imgNum ++;
                        animator.SetInteger("Image Number", imgNum);
                    }
                }
            }else {
                animator.Play("Out", 2);
            }
            animator.Play("Off", 1);
        }
    }
}
