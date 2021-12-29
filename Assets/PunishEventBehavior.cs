using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunishEventBehavior : MonoBehaviour
{
    [Tooltip("Which achievement group should the punishment come upon.")]
    public int groupIndex;
    private bool activated = true;
    private static DEMO_AchievementBehavior achievementManager;

    //用于激活满血奖励事件
    public bool reversedForRewarding = false;
    public PlayerBehavior player;

    public static void initializeAchievementPointer()
    {
        achievementManager = GameObject.Find("Achievement Manager").GetComponent<DEMO_AchievementBehavior>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && activated) {
            if(!reversedForRewarding) {
                achievementManager.punishInGroup(groupIndex);
            }else {
                //检查是否满足满血奖励（成就组3）
                if(player.isMaxHealth()) {
                    achievementManager.collectInGroup(groupIndex);
                }
            }
                
            activated = false;
            Debug.Log("PUNISHED");
        }
    }
}
