using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Input = InputHandler;
using System.Windows;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    private const int MOG = 0;
    private const int CHOCO = 1;
    private const int TONBERRY = 2;

    private const string HOST = "ChrisTenarium\n";

    private GamesListController gamesListController;

    [SerializeField] private GameObject[] TeamIcons;
    [SerializeField] private TMP_Text[] TeamNames;
    [SerializeField] private TMP_Text[] TeamRunnerNames;
    [SerializeField] private TMP_Text Commentary;

    public List<Game_SO> GameData = new();
    public List<Runner_SO> MogRunners = new();
    public List<Runner_SO> ChocoRunners = new();
    public List<Runner_SO> TonberryRunners = new();

    private int gameState = 0;
    private int currentGame = 0;

    private string[] commentators = 
    {
        "Thunderclaude", "Deln", "Zer0", "Nicky", "Zwanzig", "Dyne", "Kev", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18"
    };
    
    void Start()
    {
        gamesListController = GetComponent<GamesListController>();
        // Set first game
        ToggleTeamsDisplay(false);
        gamesListController.SetCurrentGame(currentGame);
        UpdateRunnerNames(currentGame);
        Commentary.text = HOST + commentators[0];
        
        if(GameData.Count == 0)
            Debug.LogWarning("Game list empty. Did you forget to create and assign SOs? - Use FF Header tools");
    }
    
    

    void Update()
    {
        if (!Input.NextState && !Input.PreviousState)
            return;

        if (Input.NextState)
        {
            gameState++;
            if (gameState >= 4)
            {
                gameState = 0;
                NextGame();
                return;
            }
            // Display the next team's runner and icon
            TeamIcons[gameState - 1].SetActive(true);
            TeamNames[gameState - 1].enabled = true;
            TeamRunnerNames[gameState - 1].enabled = true;
        }
        
        if (Input.PreviousState)
        {
            gameState = 4;
            PreviousGame();
            gamesListController.SetCurrentGame(currentGame);
        }
    }

    private void NextGame()
    {
        // Disable teams
        ToggleTeamsDisplay(false);

        currentGame++;
        gamesListController.SetCurrentGame(currentGame);
        UpdateRunnerNames(currentGame);
        Commentary.text = HOST + commentators[currentGame];
    }

    private void PreviousGame()
    {
        ToggleTeamsDisplay(true);
        currentGame--;
        currentGame = Mathf.Clamp(currentGame,0,GameData.Count);
        gamesListController.SetCurrentGame(currentGame);
        UpdateRunnerNames(currentGame);
        Commentary.text = HOST + commentators[currentGame];
    }

    private void SelectGame(int gameID)
    {
        ToggleTeamsDisplay(false);
        currentGame = gameID;
        currentGame = Mathf.Clamp(currentGame,0,GameData.Count);
        gamesListController.SetCurrentGame(currentGame);
        UpdateRunnerNames(currentGame);
        Commentary.text = HOST + commentators[currentGame];
    }

    private void UpdateRunnerNames(int gameID)
    {
        TeamRunnerNames[MOG].text = MogRunners[gameID].Name;
        TeamRunnerNames[CHOCO].text = ChocoRunners[gameID].Name;
        TeamRunnerNames[TONBERRY].text = TonberryRunners[gameID].Name;
    }

    private void ToggleTeamsDisplay(bool active)
    {
        for (int i = 0; i < 3; i++)
        {
            TeamIcons[i].SetActive(active);
            TeamNames[i].enabled = active;
            TeamRunnerNames[i].enabled = active;
        }
    }
}
