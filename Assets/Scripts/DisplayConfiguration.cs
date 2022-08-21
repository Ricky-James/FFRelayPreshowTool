using UnityEngine;


public class DisplayConfiguration : MonoBehaviour
{
//#if !UNITY_EDITOR
    // Start is called before the first frame update
    void Start()
    {
        //Display.displays[0].SetParams(1280,720,Screen.width /2, Screen.height / 2);
        Display.displays[0].SetRenderingResolution(2560, 1440);
        
        
        //Screen.SetResolution(1280,720,false,60);
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
        
        Screen.fullScreen = false;
    }
//#endif    
}

