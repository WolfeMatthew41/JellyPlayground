using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject pauseBtn;

    private UIManager Instance;

    private void Awake() {
        if(Instance != null){
            Destroy(this);
        }
        Instance = this;
    }

    public UIManager GetInstance(){
        return Instance;
    }

    public void PressPauseButton(){
        if(pauseBtn.GetComponentInChildren<TMP_Text>().text.Equals("Pause")){
            pauseBtn.GetComponentInChildren<TMP_Text>().text = "Resume";
        }
    }
}
