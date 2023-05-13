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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (rideWithPlatform)
        {
            if (collision.transform.tag == "Player")
            {
                player.transform.parent = this.transform;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            player.transform.parent = null;
        }
    }
}
