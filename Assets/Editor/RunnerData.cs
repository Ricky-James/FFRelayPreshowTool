using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using static Runner_SO;

public class RunnerData
{
    // Used to remove special chars from file names
    private const string fileNameConstraints = @"[<>:\/.|?*!\\]";
    private const string MogPath = "Assets/RelayFiles/mog-runners.txt";
    private const string ChocoPath = "Assets/RelayFiles/choco-runners.txt";
    private const string TonberryPath = "Assets/RelayFiles/tonberry-runners.txt";

    [MenuItem("FF Relay Tool/Runners/Create Runner Scriptable Objects")]
    public static void GenerateRunnerObjects()
    {
        // Reset directories
        Directory.Delete("Assets/Runners", true);
        Directory.CreateDirectory("Assets/Runners");
        Directory.CreateDirectory($"Assets/Runners/{Team.Mog}");
        Directory.CreateDirectory($"Assets/Runners/{Team.Choco}");
        Directory.CreateDirectory($"Assets/Runners/{Team.Tonberry}");

        if (!File.Exists(MogPath))
            throw new FileNotFoundException(MogPath);
        if (!File.Exists(ChocoPath))
            throw new FileNotFoundException(ChocoPath);
        if (!File.Exists(TonberryPath))
            throw new FileNotFoundException(TonberryPath);

        
        int runnerCount = NumberOfRunners();
        Runner_SO[] MogRunners = new Runner_SO[runnerCount];
        Runner_SO[] ChocoRunners = new Runner_SO[runnerCount];
        Runner_SO[] TonberryRunners = new Runner_SO[runnerCount];
        
        string[] mogNames = GetRunnerNames(MogPath);
        string[] chocoNames = GetRunnerNames(ChocoPath);
        string[] tonberryNames = GetRunnerNames(TonberryPath);
        
        CreateRunnerObjects(MogRunners, mogNames, Team.Mog);
        CreateRunnerObjects(ChocoRunners, chocoNames, Team.Choco);
        CreateRunnerObjects(TonberryRunners, tonberryNames, Team.Tonberry);
    }

    private static int NumberOfRunners()
    {
        string[] text = File.ReadAllLines(MogPath);
        int numRunners = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if(i % 4 == 0)
                numRunners++;
        }

        return numRunners;
    }

    private static string[] GetRunnerNames(string path)
    {
        string[] text = File.ReadAllLines(path);
        string[] runners = new string[NumberOfRunners()];

        for (int i = 0, index = 0; i < text.Length; i++)
        {
            if (i % 4 == 2)
            {
                runners[index] = text[i];
                index++;
            }
        }
        return runners;
    }

    private static void CreateRunnerObjects(Runner_SO[] runnerObject, string[] runners, Team team)
    {
        
        for(int i = 0; i < runners.Length; i++)
        {
            string runnerName = runners[i].Remove(0, 7);
            runnerObject[i] = ScriptableObject.CreateInstance<Runner_SO>();
            runnerObject[i].Name = runnerName;
            runnerObject[i].team = team;
            runnerObject[i].IsTwitch = true; //Manually configured in Inspector
 
            runnerName = Regex.Replace(runnerName, fileNameConstraints, "");
            AssetDatabase.CreateAsset(runnerObject[i], $"Assets/Runners/{team}/{i+1} - {team} - {runnerName}.asset");
            AssetDatabase.SaveAssets();
        }
    }


    
}
        
