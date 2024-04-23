using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] Button buttonPlayPause;

    [SerializeField] Sprite btnPlayImage;
    [SerializeField] Sprite btnPauseImage;

    bool canPlay = false;

    public void OnControlVideo()
    {
        if (canPlay)
        {
            videoPlayer.Play();
            buttonPlayPause.image.sprite = btnPauseImage;
            canPlay = false;
        }
        else
        {
            videoPlayer.Pause();
            buttonPlayPause.image.sprite = btnPlayImage;
            canPlay = true;
        }
    }
}
