using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour, IBreakable
{
    [SerializeField] private int lifeStages;
    [SerializeField] private List<Sprite> stageSprites;

    [SerializeField] private GameObject breakFXOne;
    [SerializeField] private GameObject breakFXTwo;
    [SerializeField] private GameObject breakFXThree;

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
            if(stageSprites.Count > stageSprites.IndexOf(currentSprite) + 1){
                currentSprite = stageSprites[stageSprites.IndexOf(currentSprite) + 1];
                gameObject.GetComponent<SpriteRenderer>().sprite = currentSprite;
            }
        }
        if (currentLife == 2){
            Instantiate(breakFXOne, transform.position, transform.rotation);
        } else if (currentLife == 1){
            Instantiate(breakFXTwo, transform.position, transform.rotation);
        } else{
            Instantiate(breakFXThree, transform.position, transform.rotation);
        }

        if(currentLife <= 0){
            Destroy(gameObject);
        }
    }
}
