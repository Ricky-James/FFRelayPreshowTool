using UnityEngine;


public class DisplayConfiguration : MonoBehaviour
{
#if !UNITY_EDITOR
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Display.displays[2].Activate();
            Display.displays[2].SetParams(800, 600, 0, 0);
        }
        catch
        {
            Display.displays[1].Activate();
            Display.displays[1].SetParams(800, 600, 0, 0);
        }
        
        Screen.fullScreen = false;
    }
#endif    
}

