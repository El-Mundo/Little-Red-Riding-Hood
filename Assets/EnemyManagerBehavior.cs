using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerBehavior : MonoBehaviour
{
    public AudioClip arrowWarningAudio, arrowShootAudio;
    public AudioClip wolfFallenAudio;

    void Start()
    {
        ArrowBehavior.InitializeArrowAudio(arrowWarningAudio, arrowShootAudio, this);
        WolfBehavior.InitializePointer(wolfFallenAudio);
        CrazyWerewolfController.InitializeBridgeSoundPointer();
    }
}
