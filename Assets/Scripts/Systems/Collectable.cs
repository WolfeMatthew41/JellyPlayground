    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private int value = 1;

    private TMP_Text coinsText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            DataManager.GetInstance().AddCoinValue(1);
            UIManager.GetInstance().UpdateUI();
            gameObject.GetComponent<Animator>().Play("LostJellyHappy");
        }
    }
}
