using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ObjectLoader : MonoBehaviour
{
    
    [MenuItem("FF Relay Tool/Games/Assign Scriptable Objects")]
    public static void AssignGameScriptableObjects()
    {
        // Gets all FF game scriptable objects by path
        List<Game_SO> games = new List<Game_SO>();
        string[] assetNames = AssetDatabase.FindAssets("", new [] {"Assets/Games"});
        foreach (string name in assetNames)
        {
            string path = AssetDatabase.GUIDToAssetPath(name);
            Game_SO game = AssetDatabase.LoadAssetAtPath<Game_SO>(path);
            games.Add(game);
        }

        // Assign objects to game controller in inspector
        GameController gameController = FindObjectOfType<GameController>();
        gameController.GameData.Clear();
        AudioController audioController = FindObjectOfType<AudioController>();
        audioController.gameList.Clear();
        for (int i = 0; i < games.Count; i++)
        {
            gameController.GameData.Add(games[i]);
            audioController.gameList.Add(games[i]);
        }

        Selection.activeObject = gameController;
        Debug.Log($"{games.Count} Scriptable Objects (Games) assigned to {gameController.name}.");
    }
    

    
    
    [MenuItem("FF Relay Tool/Runners/Assign Scriptable Objects")]
    private static void AssignRunnerScriptableObjects()
    {
        // Gets all FF game scriptable objects by path
        List<Runner_SO> runners = new List<Runner_SO>();
        string[] assetNames = AssetDatabase.FindAssets("",
            new[] {"Assets/Runners/Mog", "Assets/Runners/Choco", "Assets/Runners/Tonberry"});
        foreach (string name in assetNames)
        {
            string path = AssetDatabase.GUIDToAssetPath(name);
            Runner_SO runner = AssetDatabase.LoadAssetAtPath<Runner_SO>(path);
            runners.Add(runner);
        }
        
        // Assign objects to game controller in inspector
        GameController gameController = FindObjectOfType<GameController>();
        // Clear existing data
        gameController.MogRunners.Clear();
        gameController.ChocoRunners.Clear();
        gameController.TonberryRunners.Clear();
        
        // Assign runners to separate lists
        foreach(Runner_SO runner in runners)
        {
            switch (runner.team)
            {
                case Runner_SO.Team.Mog:
                    gameController.MogRunners.Add(runner);
                    break;
                case Runner_SO.Team.Choco:
                    gameController.ChocoRunners.Add(runner);
                    break;
                case Runner_SO.Team.Tonberry:
                    gameController.TonberryRunners.Add(runner);
                    break;
            }
        }

        // Sets Inspector to show gameController (to easily verify changes)
        Selection.activeObject = gameController;
        Debug.Log($"{runners.Count} Scriptable Objects (Runners) assigned to {gameController.name}.");
    }

}
