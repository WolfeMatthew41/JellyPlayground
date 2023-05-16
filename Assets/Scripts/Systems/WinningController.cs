using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningController : MonoBehaviour
{
    private bool playerHasWon;

    private void OnCollisionEnter2D(Collision2D other) {
        if(!playerHasWon && other.gameObject.tag.Equals("Player")){
            DataManager.GetInstance().AddLevel();
            playerHasWon = true;
        }
    }
}
