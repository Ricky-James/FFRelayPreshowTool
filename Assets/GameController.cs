using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Mono.CompilerServices.SymbolWriter;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Input = InputHandler;

public class GameController : MonoBehaviour
{
    private const int MOG = 0;
    private const int CHOCO = 1;
    private const int TONBERRY = 2;

    private GamesListController gamesListController;

    [SerializeField] private TMP_Text currentGameAndCategory;
    [SerializeField] private GameObject[] TeamIcons;
    [SerializeField] private TMP_Text[] TeamNames;
    [SerializeField] private TMP_Text[] TeamRunnerNames;
    [SerializeField] private string[] MogNames;
    [SerializeField] private string[] ChocoNames;
    [SerializeField] private string[] TonberryNames;

    public List<Game_SO> GameData = new();
    public List<Runner_SO> MogRunners = new();
    public List<Runner_SO> ChocoRunners = new();
    public List<Runner_SO> TonberryRunners = new();


    private int currentGame = 1;

    /// <summary>
    /// Objective is to have a repeating 1-2-3-4 1-2-3-4 for rightarrow inputs.
    /// Stage 1 hides, 2-3-4 are each team. Next game is loaded at 1.
    /// Left arrow will be harder
    /// </summary>

    void Start()
    {
        gamesListController = GetComponent<GamesListController>();
        // Set first game
        gamesListController.SetCurrentGame(1);
        
        if(GameData.Count == 0)
            Debug.LogWarning("Game list empty. Did you forget to create and assign SOs? - Use FF Header tools");
    }
    
    

    void Update()
    {
        if (!Input.NextState && !Input.PreviousState)
            return;
        
        if (Input.NextState)
        {
            currentGame++;
        }
        
        if (Input.PreviousState)
        {
            currentGame--;
        }
        
        // TODO: If inputcount % 4 change game
        gamesListController.SetCurrentGame(currentGame);
    }

    private void NextGame()
    {
        // Disable teams
        for (int i = 0; i < 3; i++)
        {
            TeamIcons[i].SetActive(false);
            TeamNames[i].enabled = false;
        }

        currentGame++;
        gamesListController.SetCurrentGame(currentGame);
    }

    private void NextGameState()
    {
        
    }

    private void PreviousGameState()
    {
        
    }

}
