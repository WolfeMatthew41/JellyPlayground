using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private int value = 1;
    private int coinsCollected;

    private TMP_Text coinsText;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        coinsText = GameObject.FindObjectOfType<TMP_Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            player.addCoins(value);
            coinsCollected = player.getCoins();
            coinsText.text = "Coins: " + coinsCollected;
            Destroy(gameObject);
        }
    }
}
