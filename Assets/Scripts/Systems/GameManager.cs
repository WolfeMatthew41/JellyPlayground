using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private UIManager uiManager;

    private void Awake() {
        uiManager = gameObject.GetComponent<UIManager>().GetInstance();
    }

    public void PressPauseButton(){
        if(Time.timeScale == 0f){
            Time.timeScale = 1f;
        }else{
            Time.timeScale = 0f;
        }
        uiManager.PressPauseButton();
    }
}
