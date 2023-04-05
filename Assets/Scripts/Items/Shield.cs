using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shield : MonoBehaviour
{
     bool inGround;
     Player player;
     Player2 player2;
    
    void Start()
    {   
        player=FindObjectOfType<Player>();
         if (SceneManager.GetActiveScene().name.Contains("player2_"))
        {
            player2=FindObjectOfType<Player2>();
        }
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
        
        if (other.gameObject.tag=="Player")
        {
            player.shield.SetActive(true);
            player.blink =false;
            if (GameManager.gm.player2!=null)
            {
                player2.shield.SetActive(true);
                player2.blink =false;
            }
           
            Destroy(gameObject);
        }
       
    }
}
