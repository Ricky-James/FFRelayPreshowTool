using UnityEngine;
using UnityEngine.UI;

public class DisplayConfiguration : MonoBehaviour
{
#if UNITY_EDITOR // Sets control panel to screen 1 for debugging in play mode
    void Start()
    {
        GetComponent<Canvas>().targetDisplay = 0;
        Image panel = GetComponentInChildren<Image>();
        panel.transform.localScale = new Vector3(0.3f, 0.3f, 1);
    }
    
#else
    void Start()
    {
        Display.displays[0].SetRenderingResolution(2560, 1440);
        Screen.fullScreen = false;
        
        try
        {
            Display.displays[2].Activate();
            Display.displays[2].SetParams(800, 600, 0, 0);
            GetComponent<Canvas>().targetDisplay = 2;
        }
        catch
        {
            Display.displays[1].Activate();
            Display.displays[1].SetParams(800, 600, 0, 0);
            GetComponent<Canvas>().targetDisplay = 1;
        }
        
    }
#endif
}

