using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown dropdown;

    [SerializeField]
    RectTransform selectPlayerPanel;

    [SerializeField]
    RectTransform addPlayerPanel;

    [SerializeField]
    RectTransform highScoresPanel;

    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    RectTransform popup;

    [SerializeField]
    TextMeshProUGUI welcomeText;

    [SerializeField]
    TextMeshProUGUI playerHighScoreText;

    [SerializeField]
    TextMeshProUGUI highScoreText;

    [SerializeField]
    UnityEngine.UI.Button startGameButton;

    // Start is called before the first frame update
    void Start()
    {
        startGameButton.interactable = false;
        SetDropdownList();
        if(MainManager.Instance.playerIndex != -1)
        {
            welcomeText.text = "Welcome " + MainManager.Instance.playerList[MainManager.Instance.playerIndex].name;
            playerHighScoreText.text = "Your Best: " + MainManager.Instance.playerList[MainManager.Instance.playerIndex].highScore;
        }
        if (MainManager.Instance.highScores != null && MainManager.Instance.highScores.Count > 0)
        {
            highScoreText.text = "High Score: " + MainManager.Instance.highScores[0].highScore;
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    void SetDropdownList()
    {
        List<MainManager.Player> playerList = MainManager.Instance.playerList;
        dropdown.options.Clear();
        dropdown.options.Add(new TMP_Dropdown.OptionData());
        foreach (MainManager.Player player in playerList)
        {
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
            data.text = player.name;
            dropdown.options.Add(data);
        }
    }

    public void SelectPlayer()
    {
        if (dropdown.options[dropdown.value].text != null && dropdown.options[dropdown.value].text != "")
        {
            MainManager.Instance.SelectPlayer(dropdown.options[dropdown.value].text);
            welcomeText.text = "Welcome " + MainManager.Instance.playerList[MainManager.Instance.playerIndex].name;
            playerHighScoreText.text = "Your Best: " + MainManager.Instance.playerList[MainManager.Instance.playerIndex].highScore;
            selectPlayerPanel.gameObject.SetActive(false);
            popup.gameObject.SetActive(false);
            startGameButton.interactable = true;
        }
    }

    public void AddPlayer() 
    {

        if (inputField.text != null && inputField.text != "")
        {
            MainManager.Instance.AddPlayer(inputField.text);
            addPlayerPanel.gameObject.SetActive(false);
            popup.gameObject.SetActive(false);
            Debug.Log(MainManager.Instance.playerIndex);
            welcomeText.text = "Welcome " + MainManager.Instance.playerList[MainManager.Instance.playerIndex].name;
            SetDropdownList();
            startGameButton.interactable = true;
        }
    }

    public void OpenSelectPlayer()
    {
        selectPlayerPanel.gameObject.SetActive(true);
        popup.gameObject.SetActive(true);
    }

    public void CancelSelectPlayer()
    {
        selectPlayerPanel.gameObject.SetActive(false);
        popup.gameObject.SetActive(false );
    }

    public void OpenAddPlayer()
    {
        addPlayerPanel.gameObject.SetActive(true);
        popup.gameObject.SetActive(true);
    }

    public void CancelAddPlayer()
    {
        addPlayerPanel.gameObject.SetActive(false);
        popup.gameObject.SetActive(false);
    }

    public void OpenHighScores()
    {
        highScoresPanel.gameObject.SetActive(true);
        popup.gameObject.SetActive(true);
    }

    public void CloseHighScores()
    {
        highScoresPanel.gameObject.SetActive(false);
        popup.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        MainManager.Instance.Exit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }
}
