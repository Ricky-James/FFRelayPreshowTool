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

    private const string SELECTED = "<color=#FFFFFFFF><size=125%>";
    private const string UNSELECTED = "<color=#888888FF><size=100%>";
    private const string END_FORMATTING = "</size>";

    private void Start()
    {
        GamesListText.text = "";
        foreach (Game_SO game in gameController.GameData)
        {
            
            GamesListText.text += UNSELECTED + game.Name + END_FORMATTING + "\n";
        }
        HighlightGame(5);
    }
    
    public void HighlightGame(int gameID)
    {
        try { gameID--; }
        catch
        {
            Debug.LogWarning("Tried to access game 0");
            gameID = Mathf.Clamp(gameID, 1, gameController.GameData.Count); // Index 1
        }

        ResetTextColours();
        gameController.GameData[gameID].Selected = true;
        UpdateText();
    }

    // Set all game text colours to grey
    private void ResetTextColours()
    {
        foreach (Game_SO game in gameController.GameData)
        {
            game.Selected = false;
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
            GamesListText.text += (game.Selected ? SELECTED : UNSELECTED) + i.ToString("00.##") + " - " + game.Name + END_FORMATTING + "\n";
            i++;
        }
    }


}
