using System.Collections;
using System.Collections.Generic;
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
    public string Name;
    public Team team;
    public Image flag;
    public bool IsTwitch;
}
