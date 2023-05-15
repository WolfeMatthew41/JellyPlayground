using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("Player")){
            DataManager.GetInstance().AddLevel();
        }
    }
}
