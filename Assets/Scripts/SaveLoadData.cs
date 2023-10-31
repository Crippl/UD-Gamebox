using Palmmedia.ReportGenerator.Core.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;

public class SaveLoadData : MonoBehaviour
{
    private string filePath;
    private string fileName = "Gamedata.json";
    private int playerCoin;
    [SerializeField] private LumberCamp lumberCamp;

    private void Awake()
    {
        filePath =Path.Combine(Application.dataPath, fileName);
        LoadData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void SaveData()
    {
        GameData gameData = new GameData
        {
            playerCoin = this.playerCoin,
            lumberCamp = this.lumberCamp
        };

        string json=JsonUtility.ToJson(gameData);
        File.WriteAllText(filePath, json);
    }

    private void LoadData()
    {
        if (!File.Exists(filePath)) 
        {
            Debug.Log("File not created");
            return;
        }

        string json = File.ReadAllText(filePath);
        GameData gameDataFromFile=JsonUtility.FromJson<GameData>(json);
        this.playerCoin = gameDataFromFile.playerCoin;
        this.lumberCamp = gameDataFromFile.lumberCamp;
    }
}

[System.Serializable]
public class GameData
{
    public int playerCoin;
    public LumberCamp lumberCamp;
}
