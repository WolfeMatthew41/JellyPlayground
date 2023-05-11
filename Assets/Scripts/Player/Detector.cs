using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private int overlaps;

    private void OnTriggerEnter2D(Collider2D other) {
        overlaps++;
    }

    private void OnTriggerExit2D(Collider2D other) {
        overlaps--;
    }

    public bool CanSwitch()
    {
        return overlaps <= 0;
    }

}
