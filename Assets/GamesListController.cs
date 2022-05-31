using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GamesListController : MonoBehaviour
{
    public TMP_Text GamesListText;
    public GameController gameController;

    private const string WHITE = "<alpha=#FF>";
    private const string GREY = "<alpha=#66>";

    private void Start()
    {
        GamesListText.text = "";
        foreach (Game_SO game in gameController.GameData)
        {
            
            GamesListText.text += GREY + game.Name + "\n";
        }
        HighlightGame(1);
    }

    // Set all game text colours to grey
    private void ResetTextColours()
    {
        foreach (Game_SO game in gameController.GameData)
        {
            game.TextColor = GREY;
        }
    }

    private void UpdateText()
    {
        GamesListText.text = "";
        int i = 1;
        foreach (Game_SO game in gameController.GameData)
        {
            // Numbers each game (01, 02 etc.), adds a colour using Rich Text, then adds game name
            // Example: 17 - Final Fantasy Tactics
            GamesListText.text += game.TextColor + i.ToString("00.##") + " - " + game.Name + "\n";
            i++;
        }
    }

    public void HighlightGame(int gameID)
    {
        try { gameID--; }
        catch
        {
            gameID = Mathf.Clamp(gameID, 1, gameController.GameData.Count); // Index 1
        }

        ResetTextColours();
        gameController.GameData[gameID].TextColor = WHITE;
        UpdateText();
    }
}
