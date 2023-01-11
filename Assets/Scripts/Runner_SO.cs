using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Runner_SO : ScriptableObject
{
    public enum Team
    {
        Choco,
        Mog,
        Tonberry
    }
    public enum StreamService
    {
        Twitch,
        YouTube,
        None
    }
    
    public string Name;
    public string StreamName;
    public Team team;
    public Sprite flag;
    public StreamService streamService;
}
