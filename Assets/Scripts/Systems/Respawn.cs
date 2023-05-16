using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField]
    private Transform firstRespawnPoint;
    
    private bool useCheckpoint = false;
    
    private Transform player;

    private Vector3 cameraPositionRespawn;
    private Vector3 cameraPositionCheckpoint;
    private Vector3 checkpointPosition;

    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        camera = player.GetComponentInChildren<Camera>();
        cameraPositionRespawn = camera.transform.position;
        cameraPositionCheckpoint = Vector3.zero;
        checkpointPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        camera.transform.parent = null;
        player.gameObject.SetActive(false);

        if (!useCheckpoint)
        {
            player.gameObject.transform.position = firstRespawnPoint.position;
            camera.transform.position = cameraPositionRespawn;
        } else
        {
            player.gameObject.transform.position = checkpointPosition;
            camera.transform.position = cameraPositionCheckpoint;
        }

        player.gameObject.SetActive(true);
        camera.transform.parent = player;

    }

    public void storeCheckpointData(Vector3 playerRespawnPosition)
    {
        checkpointPosition = playerRespawnPosition;
        cameraPositionCheckpoint = camera.transform.position;
    }

    public void setCheckpointBool(bool isChecked)
    {
        useCheckpoint = isChecked;
    }
}
