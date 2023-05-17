using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector2 firstRespawnPoint;
    
    private bool useCheckpoint = false;
    
    private Transform player;

    private Camera camera;
    private float cameraZ;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        firstRespawnPoint = player.transform.position;
        camera = player.GetComponentInChildren<Camera>();
        cameraZ = camera.transform.position.z;
        Debug.Log(player.transform.position);
        Debug.Log(firstRespawnPoint);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        camera.transform.parent = null;
        player.gameObject.SetActive(false);

        if (!useCheckpoint)
        {
            Debug.Log("HERE");
            player.transform.position = firstRespawnPoint;
            Debug.Log(player.transform.position);
            Debug.Log(firstRespawnPoint);
        } else
        {
            player.gameObject.transform.position = DataManager.GetInstance().GetCheckpoint().transform.position;
        }

        player.gameObject.SetActive(true);
        camera.transform.parent = player;
        camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, cameraZ);
    }

    public void setCheckpointBool(bool isChecked)
    {
        useCheckpoint = isChecked;
    }
}
