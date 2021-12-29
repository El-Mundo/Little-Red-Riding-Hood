using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBonusBehavior : MonoBehaviour
{
    bool isCollected = false;
    public int groupIndex;

    private static DEMO_ManagerBehavior manager;
    private static DEMO_AchievementBehavior achievement;

    public static void initializeManagers()
    {
        manager = GameObject.Find("Game Manager").GetComponent<DEMO_ManagerBehavior>();
        achievement = GameObject.Find("Achievement Manager").GetComponent<DEMO_AchievementBehavior>();
    }

    public void PlayerCollect()
    {
        if(!isCollected) {
            manager.addMana();
            if(groupIndex > 0) {
                achievement.collectInGroup(groupIndex);
            }
        }
        isCollected = true;
        transform.position = achievement.transform.position;
    }
}
