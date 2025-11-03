using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LoadVideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoPlayerCanvas;

    public FPSController fpsController;

    private void Awake()
    {
        if (videoPlayer != null)
            videoPlayer.loopPointReached += StopVideo;
    }

    private void Start()
    {
        videoPlayerCanvas.SetActive(false);
    }

    public void PlayVideo(string url)
    {
        if(!videoPlayer.isPlaying)
            StartCoroutine(DownloadAndPlayVideo(url));
    }

    IEnumerator DownloadAndPlayVideo(string url)
    {
        fpsController.inVideo = true;
        videoPlayerCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        videoPlayer.url = url;
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }
        videoPlayer.Play();
    }

    public void StopVideo(VideoPlayer vp)
    {
        videoPlayer.Stop();
        videoPlayerCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        fpsController.inVideo = false;
    }

}
