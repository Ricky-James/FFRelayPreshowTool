using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamesListController : MonoBehaviour
{
    public TMP_Text GamesListText;
    public GameController gameController;
    [SerializeField] private TMP_Text currentGameAndCategory;

    private const string SELECTED = "<color=#FFFFFFFF><size=145%>";
    private const string NOTDONE = "<color=#88888800><size=100%>";
    private const string DONE = "<color=#888888FF><size=100%>";
    private const string WHITE = "<color=#FFFFFFFF><size=100%>";
    private const string END_FORMATTING = "</size>";

    private void Awake()
    {
        GamesListText.text = "";
        foreach (Game_SO game in gameController.GameData)
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            { 
                case 1: // Main scene
                    GamesListText.text += NOTDONE + game.Title + END_FORMATTING + "\n";
                    break;
                case 2: // Outro scene
                    GamesListText.text += WHITE + game.Title + END_FORMATTING + "\n";
                    break;
                default:
                    return;
            }

        }
    }
    
    public void SetCurrentGame(int gameID)
    {
        ResetGamesList();
        gameController.GameData[gameID].Selected = true;
        gameController.GameData[gameID].Done = true;
        UpdateText();
    }
    
    private void ResetGamesList()
    {
        foreach (Game_SO game in gameController.GameData)
        {
            game.Selected = false;
        }
    }
    
    private void UpdateText()
    {
        // Games List text
        GamesListText.text = "";

        foreach (Game_SO game in gameController.GameData)
        {
            // Update current game text if game is selected
            if (game.Selected)
                currentGameAndCategory.text = game.Title + "\n<size=65%>" + game.Category + "</size>";

            // Formats the selected game to be bigger and coloured white
            // Bit of a monstrosity but essentially games are big and white when it's the current game,
            // Invisible when it has not been revealed
            // And grey when it's been revealed but no longer the current game
            GamesListText.text += (game.Selected ? SELECTED : game.Done ? DONE : NOTDONE) + game.Title + END_FORMATTING + "\n";
        }
    }
    
    public bool isAllGamesComplete
    {
	    get
	    {
		    foreach (Game_SO game in gameController.GameData)
		    {
			    if (!game.Done)
				    return false;
		    }

		    return true;
	    }
    }

    private void OnApplicationQuit()
    {
        foreach (Game_SO game in gameController.GameData)
        {
            ResetGamesList();
            game.Done = false;
        }
    }
    
}
