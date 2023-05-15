using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private int overlaps;


    private void Start() {
        if(gameObject.name.Equals("DDetectorR") || gameObject.name.Equals("DDetectorL")){
            PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            transform.localScale = new Vector3(playerController.GetDashIntensity() / 150 * 4, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.isTrigger)
            overlaps++;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(!other.isTrigger)
            overlaps--;
    }

    public bool NotColliding()
    {
        return overlaps <= 0;
    }

}
