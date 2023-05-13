using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject pauseBtn;
    [SerializeField] private TMP_Text coinsText;

    private static UIManager Instance;

    private void Awake() {
        if(Instance != null){
            Destroy(this);
        }
        Instance = this;
    }

    private void Start() {
        coinsText.text = "Coins: " + DataManager.GetInstance().GetCoins().ToString();
    }

    public static UIManager GetInstance(){
        return Instance;
    }

    public void PressPauseButton(){
        if(pauseBtn.GetComponentInChildren<TMP_Text>().text.Equals("Pause")){
            pauseBtn.GetComponentInChildren<TMP_Text>().text = "Resume";
        }
    }

    public void UpdateUI(){
        coinsText.text = "Coins: " + DataManager.GetInstance().GetCoins().ToString();
    }
}
