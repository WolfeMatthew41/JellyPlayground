using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    [SerializeField]
    private GameObject waypointStart;
    [SerializeField]
    private GameObject waypointEnd;
    [SerializeField]
    private bool movingToEnd = true;
    [SerializeField]
    private bool rideWithPlatform = true;
    
    [SerializeField]
    private float speed = 3.0f;

    private Transform player;
    private PlayerController playerController;

    private float platformWidth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerController = player.GetComponent<PlayerController>();
        platformWidth = GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If the moving platform is at the start waypoint and heading to the start waypoint, then make movingToEnd true.
        if (this.transform.position == waypointStart.transform.position && !movingToEnd)
        {
            movingToEnd = true;
        }

        // If the moving platform is at the end waypoint and heading to the end waypoint, then make movingToEnd false.
        if (this.transform.position == waypointEnd.transform.position && movingToEnd)
        {
            movingToEnd = false;
        }

        // If moving to the end waypoint, then update the position closer to the end waypoint.
        if (movingToEnd)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, waypointEnd.transform.position, speed * Time.deltaTime);
        }
        // if moving to the start waypoint, then update the position closer to the start waypoint.
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, waypointStart.transform.position, speed * Time.deltaTime);
        }
    }

    // If we opt for the player to ride with the platform, these will detect if the player is riding the platform, and parent the player
    // to the platform, and when they get off, unparent the player.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // This will make the platform not crush the player, forcing it off the stage by checking if the player is below the platform.
        if (collision.transform.tag == "Player" && player.position.y < this.transform.position.y)
        {
            movingToEnd = !movingToEnd;
        }

        // This, additionally, communicates with the PlayerController script to set the isRiding variable, so 
        // we can check for crushing if the player is riding atop the platform, and it crushes.
        if (rideWithPlatform)
        {
            if (collision.transform.tag == "Player")
            {
                // This if statement checks to make sure the Player is above the platform AND the Player's position
                // doesn't exceed the platform's width.
                if (player.position.y > this.transform.position.y && player.position.x > this.transform.position.x - platformWidth && 
                    player.position.x < (this.transform.position.x + platformWidth))
                {
                    playerController.setRiding(true);
                    player.transform.parent = this.transform;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerController.setRiding(false);
            player.transform.parent = null;
        }
    }

    public bool getMovingToEnd()
    {
        return movingToEnd;
    }
    public void setMovingToEnd(bool mte)
    {
        movingToEnd = mte;
    }
}
