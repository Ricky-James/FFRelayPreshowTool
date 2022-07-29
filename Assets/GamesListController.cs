using TMPro;
using UnityEngine;

public class GamesListController : MonoBehaviour
{
    public TMP_Text GamesListText;
    public GameController gameController;
    [SerializeField] private TMP_Text currentGameAndCategory;

    private const string SELECTED = "<color=#FFFFFFFF><size=145%>";
    private const string UNSELECTED = "<color=#888888FF><size=100%>";
    private const string END_FORMATTING = "</size>";

    private void Awake()
    {
        GamesListText.text = "";
        foreach (Game_SO game in gameController.GameData)
        {
            GamesListText.text += UNSELECTED + game.Title + END_FORMATTING + "\n";
        }
    }
    
    public void SetCurrentGame(int gameID)
    {
        ResetGamesList();
        gameController.GameData[gameID].Selected = true;
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
            GamesListText.text += (game.Selected ? SELECTED : UNSELECTED) + game.Title + END_FORMATTING + "\n";
        }
    }


}
