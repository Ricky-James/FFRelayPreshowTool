using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{

    [SerializeField] private GameController m_GameController;
    [SerializeField] private Button[] buttons;
    
    void Start()
    {
        for (int i = 0; i < m_GameController.GameData.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponentInChildren<TMP_Text>().text = m_GameController.GameData[i].Title;
            var index = i;
            buttons[i].onClick.AddListener(() => { OnClick(index); });
        }
    }

    void OnClick(int gameID)
    {
        m_GameController.SelectGame(gameID);
    }

}
