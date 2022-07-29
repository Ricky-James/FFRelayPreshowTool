using System;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;

public class GameData
{
    private const string fileNameConstraints = @"[<>:\/.|?*!\\]";
    // Creates all the scriptable objects for every game and names them appropriately
    [MenuItem("FF Relay Tool/Games/Create FF Scriptable Objects")]
    public static void CreateGameObjects()
    {
        // Clear directory
        Directory.Delete("Assets/Games", true);
        Directory.CreateDirectory("Assets/Games");
        
        // Used to remove special chars from file names
        
        string[] gameList = GetGamesList();
        string[] categories = GetCategories();

        // Array of scriptable objects
        Game_SO[] game = new Game_SO[NumberOfGames()];
        for(int i = 0; i < NumberOfGames(); i++)
        {
            // Name each SO by game title
            string gameName = gameList[i];
            gameName = Regex.Replace(gameName, fileNameConstraints, "");

            game[i] = ScriptableObject.CreateInstance<Game_SO>();
            game[i].Title = gameList[i];
            game[i].Category = categories[i];
            game[i].Selected = false;
            
            AssetDatabase.CreateAsset(game[i], $"Assets/Games/{i+1} - {gameName}.asset");
            AssetDatabase.SaveAssets();
        }
        Console.Write($"{NumberOfGames()} assets created in Assets/Games/");
    }

    // Retrieve all text from a "runners" relay tool file
    private static string[] GetRunnerFile()
    {
        if (File.Exists("Assets/RelayFiles/mog-runners.txt"))
        {
            return File.ReadAllLines("Assets/RelayFiles/mog-runners.txt");
        }
        throw new FileNotFoundException("Assets/RelayFiles/mog-runners.txt");
    }

    private static int NumberOfGames()
    {
        string[] text = GetRunnerFile();
        int numGames = 0;
        
        for (int i = 0; i < text.Length; i++)
        {
            if(i % 4 == 0)
                numGames++;
        }

        return numGames;
    }

    private static string[] GetGamesList()
    {
        string[] text = GetRunnerFile();
        int numGames = NumberOfGames();
        
        string[] GamesList = new string[numGames];
        
        int index = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (i % 4 == 0)
            {
                GamesList[index] = text[i];
                index++;
            }
                
        }
        return GamesList;
    }

    private static string[] GetCategories()
    {
        string[] text = GetRunnerFile();
        int numGames = NumberOfGames();

        string[] Categories = new string[numGames];
        int index = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (i % 4 == 1)
            {
                Categories[index] = text[i];
                index++;
            }
        }

        return Categories;
    }
}
