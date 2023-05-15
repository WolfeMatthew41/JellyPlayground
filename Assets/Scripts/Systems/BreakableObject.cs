using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour, IBreakable
{
    [SerializeField] private int lifeStages;
    [SerializeField] private List<Sprite> stageSprites;

    public int currentLife;
    private Sprite currentSprite;

    private void Start() {
        currentLife = lifeStages;
        currentSprite = stageSprites[0];
    }

    public void Break()
    {
        if(currentLife > 0){
            Debug.Log("Break");
            currentLife--;
            currentSprite = stageSprites[stageSprites.IndexOf(currentSprite) + 1];
            gameObject.GetComponent<SpriteRenderer>().sprite = currentSprite;
        }else{
            Debug.Log("Broken");
        }
    }
}
