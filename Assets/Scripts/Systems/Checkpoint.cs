using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private Sprite sprite;
    
    //private SpriteRenderer sr;
    private Animator anim;

    private Respawn respawnZone;

    // Start is called before the first frame update
    void Start()
    {
        //sr = transform.GetComponent<SpriteRenderer>();
        anim = transform.GetComponent<Animator>();
        respawnZone = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Respawn>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            //sr.sprite = sprite;
            anim.enabled = true;
            respawnZone.storeCheckpointData(this.transform.position);
            respawnZone.setCheckpointBool(true);
        }
    }
}
