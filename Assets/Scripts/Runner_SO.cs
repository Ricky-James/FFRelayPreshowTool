using UnityEngine;

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
    public string Pronouns;
    public Team team;
    public Sprite flag;
    public StreamService streamService;
}
