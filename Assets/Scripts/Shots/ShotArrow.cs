using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotArrow : MonoBehaviour
{
    float speed=4;
    public GameObject chainGFX;
    Vector2 startPos;
    void Start()
    {
        startPos=transform.position;
        GameObject chain=Instantiate(chainGFX, transform.position,Quaternion.identity);
        chain.transform.parent=transform;
        startPos=transform.position;
    }
   
    void Update()
    {
        transform.position+=Vector3.up*speed*Time.deltaTime;
        if ((transform.position.y-startPos.y)>=0.2f)
        {
            GameObject chain=Instantiate(chainGFX, transform.position,Quaternion.identity);
            chain.transform.parent=transform;
            startPos=transform.position;
        }

    }
    private void OnTriggerEnter2D(Collider2D collider){

        if (collider.gameObject.tag=="Ball")
        {
            collider.gameObject.GetComponent<Ball>().Split();
        }
        if (collider.gameObject.tag!="Player" && collider.gameObject.tag!="Ladder")
        {
            Destroy(gameObject);
            ShotManager.shm.DestroyShot();
        }
      
    }
}
 