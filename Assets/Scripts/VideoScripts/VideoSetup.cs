using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoSetup : MonoBehaviour
{
    [Header("Canvas Elements")]
    public Animation videoBackground;
    public Image videoImage;
    
    [Header("UI Elements")]
    public Button playButton;
    public Button stopButton;
    public Slider volumeSlider;
    public Toggle loopToggle;
    public TMP_Dropdown videoDropdown;

    public VideoPlayer videoPlayer;
    private List<string> m_Videos;

    private void Start()
    {
        PopulateDropdownList();
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });
        loopToggle.onValueChanged.AddListener(delegate { OnLoopChanged(); });
        videoPlayer.loopPointReached += OnVideoEnd;
        videoDropdown.onValueChanged.AddListener(delegate { OnDropdownChange(videoDropdown); });
        playButton.onClick.AddListener(PlayButton);
        stopButton.onClick.AddListener(StopButton);
        OnVolumeChange();
        OnLoopChanged();
        TogglePlayButtons();
    }

    private void PopulateDropdownList()
    {
        videoDropdown.ClearOptions();
        
        List<TMP_Dropdown.OptionData> videoNames = new List<TMP_Dropdown.OptionData>();
        
        m_Videos = Directory.EnumerateFiles(Application.streamingAssetsPath).Where(x => !x.EndsWith(".meta") && !x.EndsWith(".json")).ToList();
        
        foreach (string video in m_Videos)
        {
            videoNames.Add(new TMP_Dropdown.OptionData(Path.GetFileNameWithoutExtension(video)));
        }

        videoDropdown.options = videoNames;
        videoPlayer.url = m_Videos[0];
    }

    private void PlayButton()
    {
        videoImage.enabled = true;
        videoPlayer.url = m_Videos[videoDropdown.value];
        videoPlayer.Play();
        TogglePlayButtons();
        ToggleBackground();
    }

    private void StopButton()
    {
        videoImage.enabled = false;
        videoPlayer.Stop();
        TogglePlayButtons();
        ToggleBackground();
    }

    private void OnVolumeChange()
    {
        videoPlayer.SetDirectAudioVolume(0, volumeSlider.value);
    }

    private void OnLoopChanged()
    {
        videoPlayer.isLooping = loopToggle.isOn;
    }
    
    private void OnVideoEnd(VideoPlayer source)
    {
        if (!source.isLooping)
        {
            StopButton();
            TogglePlayButtons();
            ToggleBackground();
        }
    }

    private void OnDropdownChange(TMP_Dropdown dropdown)
    {
        if(videoPlayer.isPlaying)
            StopButton();
        videoPlayer.url = m_Videos[dropdown.value];
        Debug.Log($"VideoPlayer URL: {videoPlayer.url}");
    }

    private void TogglePlayButtons()
    {
        bool isPlaying = videoImage.enabled;
        playButton.enabled = !isPlaying;
        stopButton.enabled = isPlaying;
    }

    private void ToggleBackground()
    {
        videoBackground.clip = videoImage.enabled
            ? videoBackground.GetClip("DimBackground")
            : videoBackground.clip = videoBackground.GetClip("LightBackground");
        videoBackground.Play();
    }



}


