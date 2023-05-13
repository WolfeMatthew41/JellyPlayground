using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject player;

    private PlayerInputActions playerInputActions;

    private void Start() {
        playerInputActions = player.GetComponent<PlayerController>().GetPlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Pause.performed += ctx => PressPauseButton();
    }

    public void PressPauseButton(){
        if(Time.timeScale == 0f){
            Time.timeScale = 1f;
        }else{
            Time.timeScale = 0f;
        }
        UIManager.GetInstance().PressPauseButton();
    }
}
