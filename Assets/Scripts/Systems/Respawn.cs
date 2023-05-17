using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField]
    private Transform firstRespawnPoint;
    
    private bool useCheckpoint = false;
    
    private Transform player;

    private Camera camera;
    private float cameraZ;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        camera = player.GetComponentInChildren<Camera>();
        cameraZ = camera.transform.position.z;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        camera.transform.parent = null;
        player.gameObject.SetActive(false);

        if (!useCheckpoint)
        {
            player.gameObject.transform.position = firstRespawnPoint.position;
        } else
        {
            player.gameObject.transform.position = DataManager.GetInstance().GetCheckpoint().transform.position;
        }

        player.gameObject.SetActive(true);
        camera.transform.parent = player;
        camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, cameraZ);
        GameObject.Find("Green").transform.localScale = new Vector3(0.6f, 0.6f, 1f);
        GameObject.Find("Blue").transform.localScale = new Vector3(0.6f, 0.6f, 1f);
        GameObject.Find("Violet").transform.localScale = new Vector3(0.6f, 0.6f, 1f);
        GameObject.Find("Red").transform.localScale = new Vector3(0.6f, 0.6f, 1f);
    }

    public void setCheckpointBool(bool isChecked)
    {
        useCheckpoint = isChecked;
    }
}
