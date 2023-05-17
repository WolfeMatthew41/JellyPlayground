using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private Sprite sprite;

    private bool wasUsed;
    
    private Animator anim;

    private Respawn respawnZone;

    [SerializeField] private GameObject CheerSound;

    void Start()
    {
        anim = transform.GetComponent<Animator>();
        respawnZone = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Respawn>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !wasUsed)
        {
            anim.enabled = true;
            respawnZone.setCheckpointBool(true);
            DataManager.GetInstance().UpdateCheckPoint(gameObject);
            wasUsed = true;
            CheerSound.GetComponent<AudioSource>().Play();
        }
    }
}
