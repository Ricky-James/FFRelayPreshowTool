using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class IntroAudioController : MonoBehaviour
{
	[FormerlySerializedAs("songList")] [SerializeField]
	private List<AudioClip> trackList = new();
	private AudioSource audioSource;
	private float normalizedTrackLength = 0; // Equal duration of each track before changing to next
	private int currentTrackID = 0;
	private AudioController _audioController;
	private float trackTimer = 0;

	private void Start()
	{
		_audioController = GetComponent<AudioController>();
		audioSource = GetComponent<AudioSource>();
		trackList.Sort(new ReverseAudioSort());

		
		audioSource.clip = trackList[0];
		audioSource.Play();
       
		// TODO : Play next tracks in sequence, then loop final track

		// Target: 60:00 excluding Prelude
		
		normalizedTrackLength = IntroTimer.startTimeSeconds / trackList.Count;
		
#if UNITY_EDITOR
		SongValidation();
#endif
	}
	
	private void Update()
	{
		trackTimer += Time.deltaTime;
		if (IsTrackFinished)
		{
			AdvanceTrack();
		}
	}
	
	private bool IsTrackFinished => (audioSource.time >= normalizedTrackLength && trackTimer >= normalizedTrackLength) || !audioSource.isPlaying;

	
	private void AdvanceTrack()
	{
		trackTimer = 0;
		currentTrackID++;

		if (currentTrackID < trackList.Count)
		{
			audioSource.loop = currentTrackID + 1 == trackList.Count;
			StartCoroutine(_audioController.FadeOutAndChange(trackList[currentTrackID]));
		}
	}
	

	private void SongValidation()
	{
		float maxDuration = 0;
		foreach (var track in trackList)
		{
			maxDuration += track.length;
			if (track.length < normalizedTrackLength)
			{
				Debug.LogWarning($"<color=red>Track length too short: {track.name}</color>");
			}
		}

		TimeSpan timeSpan = TimeSpan.FromSeconds(maxDuration);
		Debug.Log($"Intro music duration: {String.Format("{0}:{1:00}", timeSpan.Minutes, timeSpan.Seconds)}");
	}

	
}

// Sorts by first 2 digits of the file name, in reverse order
// The relay intro typically plays the newest songs first (i.e. starting with FF16 -> FF1)
internal class ReverseAudioSort : IComparer<AudioClip>
{
	public int Compare(AudioClip x, AudioClip y)
	{
		string xPrefix = x.name.Substring(x.name.LastIndexOf('/') + 1, 2);
		string yPrefix = y.name.Substring(y.name.LastIndexOf('/') + 1, 2);

		int xNumber = int.Parse(xPrefix);
		int yNumber = int.Parse(yPrefix);

		return yNumber.CompareTo(xNumber);
	}
}
