using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningController : MonoBehaviour
{
    private bool playerHasWon;

    private void OnCollisionEnter2D(Collision2D other) {
        if(!playerHasWon && other.gameObject.tag.Equals("Player")){
            DataManager.GetInstance().AddLevel();
            playerHasWon = true;

            GetComponent<Animator>().SetTrigger("Victory");
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().GetComponentInChildren<Camera>().transform.parent = null;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().gameObject.SetActive(false);

            
        }
    }

    private void NextStage() 
    {

        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else 
        {
            //SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).name);
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
