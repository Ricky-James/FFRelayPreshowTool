using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using Input = InputHandler;

public class GameController : MonoBehaviour
{
    private List<ScriptableObject> gameData;

    public List<ScriptableObject> GameData
    {
        get => gameData;
        set
        {
            if (Application.isEditor)
                gameData = value;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(gameData.Count == 0)
            Debug.LogWarning("Game list empty. Did you forget to create and assign SOs? - Use FF Header tools");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.NextState)
        {
            
        }
    }

}
