using System;
using TMPro;
using UnityEngine;

public class IntroTimer : MonoBehaviour
{
    private TMP_Text timerText;
    private const float startTimeSeconds = 60 * 60;
    private float remainingTime;
    
    void Start()
    {
	    timerText = GetComponent<TMP_Text>();
	    remainingTime = startTimeSeconds;
	    timerText.text = TimeToString(startTimeSeconds);
    }

    void Update()
    {
	    // Adjust timer in case of emergency
	    if (Input.GetKeyDown(KeyCode.Alpha1))
		    remainingTime += 1 * 60;
	    if (Input.GetKeyDown(KeyCode.Alpha5))
		    remainingTime += 5 * 60;
	    if (Input.GetKeyDown(KeyCode.Alpha0))
		    remainingTime -= 1 * 60;
	    
	    if (remainingTime <= 0)
	    {
		    timerText.text = TimeToString(0);
		    return;
	    }
	    remainingTime -= Time.deltaTime;
	    timerText.text = TimeToString(remainingTime);
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
