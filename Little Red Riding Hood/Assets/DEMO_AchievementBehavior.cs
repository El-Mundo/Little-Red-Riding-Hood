using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEMO_AchievementBehavior : MonoBehaviour
{
    [Tooltip("数组编号代表金币分组号，存储的整数表示在此组中收集多少金币可获得成就。")]
    public int[] collectableGroups;

    /// <summary>
    /// 每个分组中，玩家已经收集到的金币数
    /// </summary>
    private int[] collectInGroups;

    /// <summary>
    /// 达成的成就数
    /// </summary>
    private bool[] achievements;

    // Start is called before the first frame update
    void Start()
    {
        collectInGroups = new int[collectableGroups.Length];
        achievements = new bool[collectableGroups.Length];
    }

    public void collectInGroup(int index)
    {
        if(collectableGroups.Length >= index) {
            collectInGroups[index - 1] ++;

            Debug.Log("COLLECTION IN #" + index + ", " + collectInGroups[index - 1] + "/" + collectableGroups[index - 1]);
        }
    }

    public void punishInGroup(int index)
    {
        if(collectableGroups.Length >= index) {
            //在此成就未达成时，触发成就惩罚事件，清空成就完成进度
            if(collectInGroups[index - 1] < collectableGroups[index - 1]) {
                collectInGroups[index - 1] = 0;
            }
        }
    }

    public bool[] getAchievements()
    {
        for (int i = 0; i < collectableGroups.Length; i ++) {
            achievements[i] = (collectInGroups[i] >= collectableGroups[i]);
            Debug.Log("ACH #" + i + "SCC: " + achievements[i]);
        }

        return achievements;
    }
}
