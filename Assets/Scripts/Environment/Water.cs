using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private BoxCollider2D bCollider;

    private PlayerController player;

    private bool isVertical = false;
    private bool inWater = false;

    // Start is called before the first frame update
    void Start()
    {
        bCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isVertical = player.getVertical();
        inWater = player.getInWater();

        if (collision.transform.tag == "Player" && !inWater)
        {
            if (collision.transform.tag == "Player" && !isVertical)
            {
                Debug.Log("Is NOT Vertical");
                bCollider.isTrigger = false;
                player.setInWater(false);
                player.setJumpHeight(12f);
            }
            else if (collision.transform.tag == "Player" && isVertical)
            {
                Debug.Log("Is Vertical");
                bCollider.isTrigger = true;
                player.setInWater(true);
                player.setJumpHeight(15f);
            }
        }
    }
}
