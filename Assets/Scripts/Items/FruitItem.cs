using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitItem : MonoBehaviour
{
     bool inGround;
     public Sprite[] fruitSprites;

     SpriteRenderer sr;
    
      private void Awake()
    {
        sr= GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        sr.sprite = fruitSprites[Random.Range(0, fruitSprites.Length)];
        gameObject.name=sr.sprite.name;
    }

    void Update()
    {
        if (!inGround)
        {
            transform.position += Vector3.down*Time.deltaTime*2;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag=="Ground")
        {
            inGround=true;     
            Destroy(gameObject, 20);    
        }
        
        if (other.gameObject.tag=="Player" || other.gameObject.tag=="Arrow"
        || other.gameObject.tag=="Ancle" || other.gameObject.tag=="Laser")
        {   
            int score=Random.Range(20,101);
            ScoreManager.sm.UpdateScore(score);
            PopUpManager.pm.creaPopUpText(transform.position, score);
            GameManager.gm.fruitsCatched++;
            Destroy(gameObject);
            
            
        }
        
    }
}