using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private int overlaps;

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
