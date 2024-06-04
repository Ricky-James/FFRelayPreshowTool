using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class AudioController : MonoBehaviour
{
	public List<Game_SO> gameList;
	public Slider volumeSlider;
	private AudioClip[] trackList;
	private AudioSource audioSource;

	private bool fadeIn;
	private const float fadeDuration = 1.5f;

	private float startVolume;

	private Game_SO currentGame;
	private int currentTrackID = 0;

	private void Start()
	{
		
		audioSource = GetComponent<AudioSource>();
		
		startVolume = volumeSlider.value; // Set default volume from slider
		
		// Start muted and fade in
		audioSource.volume = 0;
		SlowUnmute();
		audioSource.loop = false;
		
		volumeSlider.onValueChanged.AddListener(delegate { OnVolumeSliderChange(); });
		
	}

	public void SetGame(Game_SO game)
	{
		currentGame = game;
		currentTrackID = 0;
		SetTrack(game, currentTrackID);
	}

	
	private void OnVolumeSliderChange()
	{
		startVolume = volumeSlider.value;
		audioSource.volume = volumeSlider.value;
	}

	// Set track for a new game, i.e. set track 0
	private void SetTrack(Game_SO game, int trackID = 0)
	{
		if (trackID >= game.musicTrack.Length)
		{
			trackID = 0;
		}
		StartCoroutine(FadeOutAndChange(game != null ? game.musicTrack[trackID] : null));
	}

	// Fade out to pause
	public void SlowMute()
	{
		StartCoroutine(FadeAudioSource(audioSource, fadeDuration, 0));
	}

	// Fade in to pause
	public void SlowUnmute()
	{
		StartCoroutine(FadeAudioSource(audioSource, fadeDuration, startVolume));
	}

	public IEnumerator FadeOutAndChange(AudioClip newClip)
	{
		// Only fade out if currently playing
		if (audioSource.isPlaying)
		{
			yield return FadeAudioSource(audioSource, fadeDuration, 0);
		}
		StartCoroutine(PlayTrack(newClip));
		yield return FadeAudioSource(audioSource, fadeDuration, startVolume);
		
	}

	private static IEnumerator FadeAudioSource(AudioSource audioSource, float duration, float targetVolume)
	{
		var startVolume = audioSource.volume;
		var startTime = Time.time;

		// Linear interpolate volume for audioSource over duration up (or down) to targetVolume.
		while (Time.time < startTime + duration)
		{
			audioSource.volume = Mathf.Lerp(startVolume, targetVolume, (Time.time - startTime) / duration);
			yield return null;
		}

		audioSource.volume = targetVolume;
	}

	private IEnumerator PlayTrack(AudioClip clip)
	{
		audioSource.clip = clip;
		audioSource.Play();
		yield return new WaitForSeconds(clip.length - fadeDuration);
		
		// Exit early for intro scene
		if (SceneManager.GetActiveScene().buildIndex == 0)
			yield break;
		
		currentTrackID++;
		SetTrack(currentGame, currentTrackID);
		yield return null;
	}
}
