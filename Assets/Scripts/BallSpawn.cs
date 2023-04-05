using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour
{

    public static BallSpawn bs;
    public GameObject[] ballsPrefab;
    GameObject ball=null;
    public bool free;
    int dificulty=1;
    float timeSpawn=20;

    void Awake()
    {
        if(bs==null){
            bs=this;
        }else if(bs!=null){
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (ball != null && ball.transform.position.y <= 4.4f && !free)
        {
            free=true;
            ball.GetComponent<Ball>().StartForce(ball);
             BallManager.bm.balls.Add(ball);
            ball.gameObject.tag="Ball";
            ball=null;
           
        }
    }

    //Instaciar la bola a lo largo del eje X de -7.25 a 7.25
    public void NextBall(){
        if (!FreezeManager.fm.freeze && ball == null )
        {
            ball=Instantiate(ballsPrefab[Random.Range(0,ballsPrefab.Length)], new Vector2(AleatoryPosition(),transform.position.y),Quaternion.identity);
            
            ball.gameObject.tag="Untagged";
            StartCoroutine(MoveDown());
        }
    }

    float AleatoryPosition(){
        return (Random.Range(-7.25f,7.25f));
    }

    public void IncreaseDificulty(){
        dificulty++;
        if (dificulty>=5 && dificulty<10)
        {
            timeSpawn=15;
        }else if (dificulty >=10 && dificulty<50)
        {
            timeSpawn=10;
        }else  if(dificulty >=50 ){
            timeSpawn=5;
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag=="Untagged")
        {
            free=false;
        }

    }
    public IEnumerator MoveDown()
    {
        if (ball!=null)
        {  
            yield return new WaitForSeconds(1);
            while (!free)
            {
                if (FreezeManager.fm.freeze)
                {
                    break;
                }
                ball.transform.position= new Vector2(ball.transform.position.x,ball.transform.position.y - 0.5f);
                yield return new WaitForSeconds(1);
            }    
            yield return new WaitForSeconds(timeSpawn);    
            if (free)
            {
                NextBall();
            }
        }
    }
}
