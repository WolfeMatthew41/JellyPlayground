using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    private static DataManager Instance;

    [SerializeField] private bool _startAtZero;
    private int _level;
    private GameObject _checkpoint;

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

    public void UpdateCheckPoint(GameObject checkpoint){
        _checkpoint = checkpoint;
        SaveCheckPoint();
    }

    public GameObject GetCheckpoint(){
        return _checkpoint;
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
        public GameObject checkpoint;
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

    public void SaveCheckPoint()
    {
        SaveData data = SaveRest();
        data.checkpoint = _checkpoint;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadCheckPoint()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            _checkpoint = data.checkpoint;
        }
    }

    private SaveData SaveRest(){
        SaveData d = new SaveData();
        d.level = _level;
        d.checkpoint = _checkpoint;
        return d;
    }

}


