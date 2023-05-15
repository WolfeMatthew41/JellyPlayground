using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void NewGame() 
    {
        //This will load the next scene after the title screen which should be the playable stage
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueGame() 
    { 
        //Will probably need to use the save data to load here
    }

    public void QuitGame()
    { 
        Application.Quit();
    }
}
