using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroTimer : MonoBehaviour
{
    private TMP_Text timerText;
    public const float startTimeSeconds = 60 * 60;
    private float remainingTime;
    public Button StartButton;
    
    void Start()
    {
	    timerText = GetComponent<TMP_Text>();
	    remainingTime = startTimeSeconds;
	    timerText.text = TimeToString(startTimeSeconds);
	    StartButton.interactable = false;
    }

    void Update()
    {
	    if (remainingTime <= 0)
	    {
		    StartButton.interactable = true;
		    timerText.text = TimeToString(0);
		    return;
	    }
	    remainingTime -= Time.deltaTime;
	    timerText.text = TimeToString(remainingTime);
    }

    // Time adjustment buttons on UI
    public void AdjustTimer(int time)
    {
	    remainingTime += time;
    }

    private string TimeToString(float time)
    {
	    TimeSpan t = TimeSpan.FromSeconds(time);
	    if (time > 60 * 60)
	    {
		    return string.Format("{0:D2}:{1:D2}:{2:D2}",t.Hours, t.Minutes, t.Seconds);
	    }
	    return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }
}
