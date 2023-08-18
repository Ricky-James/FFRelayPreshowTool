using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Input = InputHandler;


public class GameController : MonoBehaviour
{
    private const int MOG = 0;
    private const int CHOCO = 1;
    private const int TONBERRY = 2;

    private const string HOST = "ChrisTenarium\n";

    private readonly string[] commentators = {"AdrianMamba", "Talbrik", "BrainBugged", "KingSPi", "KeeperBK", "FoxyJira", "eLmaGus", "Desch",
        "Z3R01337", "sonicglenjamin", "IonicKarma", "Roosta", "Woady", "Kyoslilmonster",
        "Daspharaoh", "thebroodles", "Melle", "MrZwanzig", "Bdewd" };


    private GamesListController gamesListController;

    [SerializeField] private GameObject[] TeamIcons = new GameObject[3];
    [SerializeField] private TMP_Text[] TeamNames = new TMP_Text[3];
    [SerializeField] private TMP_Text[] TeamRunnerNames = new TMP_Text[3];
    [SerializeField] private SpriteRenderer[] TeamFlags = new SpriteRenderer[3];
    [SerializeField] private TMP_Text Commentary;
    [SerializeField] private Image commentaryIcon;
    [SerializeField] private SpriteRenderer[] StreamIcons;
    [SerializeField] private Sprite TwitchIcon;
    [SerializeField] private Sprite YouTubeIcon;

    public List<Game_SO> GameData = new();
    public List<Runner_SO> MogRunners = new();
    public List<Runner_SO> ChocoRunners = new();
    public List<Runner_SO> TonberryRunners = new();

    private int gameState = 0;
    private int currentGame = 0;
    private int gamesDoneCount = 0;


    
    void Start()
    {
        gamesListController = GetComponent<GamesListController>();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            // Set first game
            ToggleTeamsDisplay(false);
            Commentary.text = HOST;
            commentaryIcon.enabled = true;
       
            if(GameData.Count == 0)
                Debug.LogWarning("Game list empty. Did you forget to create and assign SOs? - Use FF Header tools");
        }

    }
    
    

    void Update()
    {
        if (Input.NextState && commentaryIcon.enabled) // Check first game has been selected
        {
            gameState++;
            
            if(gameState >= 4){
                gameState = 0;
                // Allows cycling through all of the games once all are revealed
                if (gamesDoneCount == GameData.Count)
                {
                    NextGame();
                }
                return;
            }

            UpdateCommentatorNames();
            // Display the next team's runner and icon
            TeamIcons[gameState - 1].SetActive(true);
            TeamNames[gameState - 1].enabled = true;
            TeamRunnerNames[gameState - 1].gameObject.SetActive(true);;
        }
    }

    private void NextGame()
    {
        ToggleTeamsDisplay(false);
        currentGame++;
        gamesListController.SetCurrentGame(currentGame);
        UpdateRunners(currentGame);
    }

    public void SelectGame(int gameID)
    {
        ToggleTeamsDisplay(false);
        currentGame = gameID;
        currentGame = Mathf.Clamp(currentGame,0,GameData.Count);
        gamesListController.SetCurrentGame(currentGame);
        UpdateRunners(currentGame);
        gameState = 0;
        UpdateCommentatorNames();
        gamesDoneCount = GameData.Count(game => game.Done);
        commentaryIcon.enabled = true;
    }

    private void UpdateCommentatorNames()
    {
        Commentary.text = gameState < 3 ? HOST : HOST + commentators[currentGame];
    }

    private void UpdateRunners(int gameID)
    {
        // Assign names
        TeamRunnerNames[MOG].text = MogRunners[gameID].Name + "<size=30%>";
        TeamRunnerNames[CHOCO].text = ChocoRunners[gameID].Name + "<size=30%>";
        TeamRunnerNames[TONBERRY].text = TonberryRunners[gameID].Name + "<size=30%>";

        // Assign stream names
        if (MogRunners[gameID].StreamName != "")
        {
            TeamRunnerNames[MOG].text += "\n" + MogRunners[gameID].StreamName;
        }
        if (ChocoRunners[gameID].StreamName != "")
        {
            TeamRunnerNames[CHOCO].text += "\n" + ChocoRunners[gameID].StreamName;
        }
        if (TonberryRunners[gameID].StreamName != "")
        {
            TeamRunnerNames[TONBERRY].text += "\n" + TonberryRunners[gameID].StreamName;
        }
        
        // Assign pronouns
        if (MogRunners[gameID].Pronouns != "")
        {
            TeamRunnerNames[MOG].text += "\n" + MogRunners[gameID].Pronouns + "</size>";
        }
        if (ChocoRunners[gameID].Pronouns != "")
        {
            TeamRunnerNames[CHOCO].text += "\n" + ChocoRunners[gameID].Pronouns + "</size>";
        }
        if (TonberryRunners[gameID].Pronouns != "")
        {
            TeamRunnerNames[TONBERRY].text += "\n" + TonberryRunners[gameID].Pronouns + "</size>";
        }
        
        UpdateRunnerIcons(currentGame);
    }

    // Flag and Stream icons
    private void UpdateRunnerIcons(int gameID)
    {
        // Assign flag
        TeamFlags[MOG].sprite = MogRunners[gameID].flag;
        TeamFlags[CHOCO].sprite = ChocoRunners[gameID].flag;
        TeamFlags[TONBERRY].sprite = TonberryRunners[gameID].flag;

        // Assign stream icon
        StreamIcons[MOG].sprite = MogRunners[gameID].streamService == Runner_SO.StreamService.Twitch
            ? TwitchIcon
            : YouTubeIcon;
        StreamIcons[CHOCO].sprite = ChocoRunners[gameID].streamService == Runner_SO.StreamService.Twitch
            ? TwitchIcon
            : YouTubeIcon;
        StreamIcons[TONBERRY].sprite = TonberryRunners[gameID].streamService == Runner_SO.StreamService.Twitch
            ? TwitchIcon
            : YouTubeIcon;
    }

    private void ToggleTeamsDisplay(bool active)
    {
        for (int i = 0; i < 3; i++)
        {
            TeamIcons[i].SetActive(active);
            TeamNames[i].enabled = active;
            TeamRunnerNames[i].gameObject.SetActive(active);
        }
    }
    
    public string GetRunners(int teamID) // Return list of runner names for provided team
    {
        List<Runner_SO> runners;
        switch (teamID)
        {
            case MOG:
                runners = MogRunners;
                break;
            case CHOCO:
                runners = ChocoRunners;
                break;
            case TONBERRY:
                runners = TonberryRunners;
                break;
            default:
                throw new Exception();
        }

        string runnerNames = "";
        for(int i = 0; i < runners.Count; i++)
        {
            runnerNames += runners[i].Name + "\n";
        }
        return runnerNames;
    }
}
