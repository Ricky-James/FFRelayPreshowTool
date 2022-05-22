using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ObjectLoader : MonoBehaviour
{
    
    [MenuItem("FF Relay Tool/Games/Assign Scriptable Objects")]
    public static void AssignScriptableObjects()
    {
        // Gets all FF game scriptable objects by path
        List<ScriptableObject> games = new List<ScriptableObject>();
        string[] assetNames = AssetDatabase.FindAssets("", new [] {"Assets/Games"});
        foreach (string name in assetNames)
        {
            string path = AssetDatabase.GUIDToAssetPath(name);
            ScriptableObject game = AssetDatabase.LoadAssetAtPath<Game_SO>(path);
            games.Add(game);
        }
        SetGameData(games);
    }

    // Assigns scriptable objects to game data in inspector
    private static void SetGameData(List<ScriptableObject> objects)
    {
        GameController gameController = FindObjectOfType<GameController>();
        gameController.GameData.Clear();
        for (int i = 0; i < objects.Count; i++)
        {
            gameController.GameData.Add(objects[i]);
            gameController.GameData[i] = objects[i];
        }

        Selection.activeObject = gameController;
        Debug.Log($"{objects.Count} Scriptable Objects assigned to {gameController.name}.");
    }

}
