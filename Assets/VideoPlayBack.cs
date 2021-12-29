using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoPlayBack : MonoBehaviour
{
    void Start()
    {
        GetComponent<UnityEngine.Video.VideoPlayer>().loopPointReached += VideoEnd;
    }

    void VideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene("TestFolder/Implicit Tutorial/Menu");
    }
}
