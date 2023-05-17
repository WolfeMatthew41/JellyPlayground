using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private BoxCollider2D bCollider;
    private BoxCollider2D cCollider;

    private PlayerController player;

    private bool isVertical = false;
    private bool inWater = false;

    private bool currForm;
    

    [SerializeField] private float launchIntensity = 0.5f;
    [SerializeField] GameObject splashSound;


    // Start is called before the first frame update
    void Start()
    {
        bCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        currForm = player.getVertical();
    }



    private void OnTriggerStay2D(Collider2D collision) 
    {
        //Debug.Log("Here Dog!");
        if (collision.transform.tag == "Player" && !isVertical && !inWater)
        {
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, launchIntensity), ForceMode2D.Impulse);
        }

        if (currForm != player.getVertical())
        {
            currForm = !currForm;
            bCollider.enabled = false;
            bCollider.enabled = true;
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
            splashSound.GetComponent<AudioSource>().Play();
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
