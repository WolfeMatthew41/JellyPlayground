using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private Sprite sprite;
    
    private SpriteRenderer sr;

    private Respawn respawnZone;

    // Start is called before the first frame update
    void Start()
    {
        sr = transform.GetComponent<SpriteRenderer>();
        respawnZone = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Respawn>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            sr.sprite = sprite;
            respawnZone.storeCheckpointData(this.transform.position);
            respawnZone.setCheckpointBool(true);
        }
    }
}
