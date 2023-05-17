using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector2 firstRespawnPoint;
    private Vector2 respawnPoint;
    
    private bool useCheckpoint = false;

    private bool isRespawn = false;
    
    private Transform player;

    private Camera camera;
    private float cameraZ;

    [SerializeField] private GameObject cloudJelly;
    private GameObject placedCloudJelly;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        firstRespawnPoint = player.transform.position;
        camera = player.GetComponentInChildren<Camera>();
        cameraZ = camera.transform.position.z;
        //Debug.Log(player.transform.position);
        //Debug.Log(firstRespawnPoint);
    }
 
    void Update() 
    {
        if (isRespawn) 
        {
            if (respawnPoint == new Vector2(player.transform.position.x, player.transform.position.y)) 
            {
                isRespawn = false;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                player.GetComponent<BoxCollider2D>().enabled = true;
                player.GetComponent<PlayerController>().enabled = true;

                placedCloudJelly.GetComponent<Animator>().SetTrigger("Clear");
            }

            player.transform.position= Vector2.MoveTowards(player.transform.position, respawnPoint, 10 * Time.deltaTime);
            Vector2 pos = respawnPoint;
            pos.y += 3;
            placedCloudJelly.transform.position= Vector2.MoveTowards(player.transform.position, pos, 10 * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //camera.transform.parent = null;
        //player.gameObject.SetActive(false);

        //player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;

        if (placedCloudJelly == null) 
        {
            placedCloudJelly = Instantiate(cloudJelly, player.transform.position, Quaternion.identity);
        }
        

        if (!useCheckpoint)
        {
            //Debug.Log("HERE");
            //player.transform.position = firstRespawnPoint;
            respawnPoint = firstRespawnPoint;
            //Debug.Log(player.transform.position);
            //Debug.Log(firstRespawnPoint);
        } else
        {
            //player.gameObject.transform.position = DataManager.GetInstance().GetCheckpoint().transform.position;
            respawnPoint= DataManager.GetInstance().GetCheckpoint().transform.position;
        }

        //player.gameObject.SetActive(true);
        //camera.transform.parent = player;
        //camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, cameraZ);
        isRespawn = true;
    }

    public void setCheckpointBool(bool isChecked)
    {
        useCheckpoint = isChecked;
    }
}
