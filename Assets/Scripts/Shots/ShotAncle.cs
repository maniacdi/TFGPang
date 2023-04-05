using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAncle : MonoBehaviour
{
    float speed=4;
    public GameObject chainGFX;
    Vector2 startPos;
    List <GameObject> chains= new List<GameObject>();
    void Start()
    {
        startPos=transform.position;
        GameObject chain=Instantiate(chainGFX, transform.position,Quaternion.identity);
        chain.transform.parent=transform;
        chains.Add(chain);
        startPos=transform.position;
    }
    
    void Update()
    {
        transform.position+=Vector3.up*speed*Time.deltaTime;
        if ((transform.position.y-startPos.y)>=0.2f)
        {
            GameObject chain=Instantiate(chainGFX, transform.position,Quaternion.identity);
            chain.transform.parent=transform;
            chains.Add(chain);
            startPos=transform.position;
        }

    }
    private void OnTriggerEnter2D(Collider2D collider){
       
        if(collider.gameObject.tag=="Roof" || collider.gameObject.tag=="Platform"){
            StartCoroutine(DestroyAncla());
        }
        if (collider.gameObject.tag=="Ball")
        {
            collider.gameObject.GetComponent<Ball>().Split();
            Destroy(gameObject);
            ShotManager.shm.DestroyShot();

        }
    }

    IEnumerator DestroyAncla(){
               
        speed=0;
        yield return new WaitForSeconds(1);
        GetComponentInParent<SpriteRenderer>().color=Color.red;
        foreach (GameObject item in chains){
            item.GetComponent<SpriteRenderer>().color=Color.red;
        }
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        ShotManager.shm.DestroyShot();
    }

}
