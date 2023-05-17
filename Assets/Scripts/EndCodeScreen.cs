using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class EndCodeScreen : MonoBehaviour
{


    public void OnExit() 
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
