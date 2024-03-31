using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public List<Player> playerList;
    public List<Player> highScores;

    public int playerIndex;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);


        playerList = new List<Player>();
        highScores = new List<Player>();
        playerIndex = -1;
        LoadPlayerList();
    }

    [System.Serializable]
    public class Player
    {
        public string name;
        public int highScore = 0;
    }

    [System.Serializable]
    public class SaveData
    {
        public List<Player> playerList;
        public List<Player> highScores;
    }

    public void LoadPlayerList()
    {
        string path = Application.persistentDataPath + "/players.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            playerList = saveData.playerList;
            highScores = saveData.highScores;
        }
        
    }

    public void Save()
    {
        SaveData saveData = new SaveData();
        saveData.playerList = playerList;
        saveData.highScores = highScores;

        string json = JsonUtility.ToJson(saveData);
        
        File.WriteAllText(Application.persistentDataPath + "/players.json", json);
    }

    public void AddPlayer(string name)
    {
        Player newPlayer = new Player();
        newPlayer.name = name;
        newPlayer.highScore = 0;
        playerList.Add(newPlayer);
        playerIndex = playerList.Count - 1;
        Save();
    }

    public void SelectPlayer(string name)
    {
        

        playerIndex = playerList.FindIndex(x => x.name == name);
        Save();
    }

    public bool SubmitScoreAndCheckIfHighScore(int score)
    {
        bool isHighScore = false;

        if (highScores.Count == 0 || score > highScores[0].highScore)
        {
            isHighScore = true;
        }
        bool scoreAdded = false;
        Player newPlayer = new Player();
        newPlayer.name = playerList[playerIndex].name;
        newPlayer.highScore = score;
        Debug.Log(score);
        for (int i = 0; i < highScores.Count; i++)
        {
            if (score > highScores[i].highScore)
            {
                
                highScores.Insert(i, newPlayer);
                scoreAdded = true;
                break;
            }
        }
        if(!scoreAdded)
        {
            highScores.Add(newPlayer);
        }
        if (highScores.Count > 10)
        {
            highScores.RemoveAt(highScores.Count - 1);
        }

        if (score > playerList[playerIndex].highScore)
        {
            playerList[playerIndex].highScore = score;
        }
        Save();
        return isHighScore;
    }



    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }
}
