using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputButtonScript : MonoBehaviour
{
    public PlayerBehavior player;
    public bool isJumpButton;

    void OnPointerEnter(PointerEventData pointer)
    {
        if(isJumpButton) {
            player.PressJump();
        }else {
            player.PressSlide();
        }
    }
}
