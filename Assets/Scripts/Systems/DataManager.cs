using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    private static DataManager Instance;

    [SerializeField] private bool _startAtZero;
    private int _level;
    private int _coins;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if(!_startAtZero){
            LoadLevel();
            LoadCoins();
        }
    }

    public void SetCurrentCoins(int coins){
        _coins = coins;
        SaveCoins();
    }

    public void AddCoinValue(int value){
        _coins += value;
        SaveCoins();
    }

    public void AddLevel(){
        _level ++;
        SaveLevel();
    }

    public int GetCoins(){
        return _coins;
    }

    public int GetCurrentLevel(){
        return _level;
    }

    public static DataManager GetInstance(){
        return Instance;
    }

    public void ResetData(){
        _coins = 0;
        _level = 0;
        SaveCoins();
        SaveLevel();
    }

    [System.Serializable]
    public class SaveData
    {
        public int level;
        public int coins;
    }

    public void SaveLevel()
    {
        SaveData data = SaveRest();
        data.level = _level;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadLevel()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            _level = data.level;
        }
    }

    public void SaveCoins()
    {
        SaveData data = SaveRest();
        data.coins = _coins;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadCoins()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            _coins = data.coins;
        }
    }

    private SaveData SaveRest(){
        SaveData d = new SaveData();
        d.level = _level;
        d.coins = _coins;
        return d;
    }

}


