using UnityEngine;
using TMPro;

public class TeamListGenerator : MonoBehaviour
{
    [SerializeField] private TMP_Text[] teamText = new TMP_Text[3];
    [SerializeField] private GameController gameController;

    void Start()
    {
        teamText[0].text = gameController.GetRunners(0);
        teamText[1].text = gameController.GetRunners(1);
        teamText[2].text = gameController.GetRunners(2);
    }
}
