using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void NewGame() 
    {
        DataManager.GetInstance().ResetData();
        //This will load the next scene after the title screen which should be the playable stage
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueGame() 
    { 
        if(DataManager.GetInstance().GetCurrentLevel() != 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 + DataManager.GetInstance().GetCurrentLevel());
        }
    }

    public void QuitGame()
    { 
        Application.Quit();
    }
}
