using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonBehavior : MonoBehaviour
{
    public bool activated = true;
    public DEMO_ManagerBehavior managerBehavior;

    public void Push()
    {
        if(activated) {
            managerBehavior.Pause();
        }else {
            managerBehavior.ActivatePause();
        }
    }
}
