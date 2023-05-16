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



    private void OnTriggerStay2D(Collider2D collision) 
    {
        //Debug.Log("Here Dog!");
        if (collision.transform.tag == "Player" && !isVertical && !inWater)
        {
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 0.5f), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.transform.tag == "Player") 
        {
            bCollider.enabled = true;
            player.setInWater(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isVertical = player.getVertical();
        inWater = player.getInWater();


        if (collision.transform.tag == "Player" && !inWater)
        {

            if (collision.transform.tag == "Player" && !isVertical) //Single-File
            {
                bCollider.enabled = true;
                //Debug.Log("Is NOT Vertical");
                //bCollider.isTrigger = false;
                //player.setInWater(false);
                //player.setJumpHeight(12f);
            }
            else if (collision.transform.tag == "Player" && isVertical) //Stacked
            {
                bCollider.enabled = false;
                //Debug.Log("Is Vertical");
                bCollider.isTrigger = true;
                player.setInWater(true);
                //player.setJumpHeight(15f);
            }
        }
    }
}
