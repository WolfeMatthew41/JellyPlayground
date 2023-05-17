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
        }
    }

    public void AddLevel(){
        _level ++;
        SaveLevel();
    }

    public int GetCurrentLevel(){
        return _level;
    }

    public static DataManager GetInstance(){
        return Instance;
    }

    public void ResetData(){
        _level = 0;
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

    private SaveData SaveRest(){
        SaveData d = new SaveData();
        d.level = _level;
        return d;
    }

}


