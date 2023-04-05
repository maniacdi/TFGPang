using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinamita : MonoBehaviour
{
   
     bool inGround;
    
    void Start()
    {
        
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
            Destroy(gameObject);
            BallManager.bm.Dinamita(5);
        }
        
    }
}