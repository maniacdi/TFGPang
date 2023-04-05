using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public GameObject nextBall;
    Rigidbody2D rb;
    public bool derch;
    Vector2 saveSpeed;
    public GameObject premio;

    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }
    public void Split(){

        GameManager.gm.PanicProgress();
        if (nextBall!=null)
        {
            CreaPremio();
            GameObject b1=Instantiate(nextBall, rb.position+Vector2.right/4, Quaternion.identity);
            GameObject b2=Instantiate(nextBall, rb.position+Vector2.left/4, Quaternion.identity);            
            if (!FreezeManager.fm.freeze)
            {
                b1.GetComponent<Rigidbody2D>().isKinematic=false;
                b1.GetComponent<Rigidbody2D>().AddForce(new Vector2(2,5), ForceMode2D.Impulse);
                b1.GetComponent<Ball>().derch=true; 
                b2.GetComponent<Rigidbody2D>().isKinematic=false;
                b2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2,5), ForceMode2D.Impulse);
                b2.GetComponent<Ball>().derch=false; 
            }else{
                b1.GetComponent<Ball>().saveSpeed=new Vector2(2,5);
                b2.GetComponent<Ball>().saveSpeed=new Vector2(-2,5);
            }
            if (!BallManager.bm.destruyendo)
            {
                BallManager.bm. DestroyBall( gameObject, b1, b2);
            }
            
        }else
        {
            BallManager.bm.LastBall(gameObject);
        }
        int score = Random.Range(10,301);
        PopUpManager.pm.creaPopUpText(gameObject.transform.position, score);
        ScoreManager.sm.UpdateScore(score);
        GameManager.gm.UpdateBalls();
    }
    public void StartForce( GameObject ball){
        ball.GetComponent<Rigidbody2D>().isKinematic=false;
       if (!SceneManager.GetActiveScene().name.Contains("_03"))
       {           
            if (derch)
            {
                ball.GetComponent<Rigidbody2D>().AddForce(Vector3.right*2, ForceMode2D.Impulse);
            }else{
                ball.GetComponent<Rigidbody2D>().AddForce(Vector3.left*2, ForceMode2D.Impulse);
            }

        }
        
    }

    public void FreezeBall(params GameObject[] balls){
        foreach (GameObject item in balls)
        {
            if (item!=null)
            {
                saveSpeed=item.GetComponent<Rigidbody2D>().velocity;
                item.GetComponent<Rigidbody2D>().isKinematic=true;
                item.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
            }
        }
    }

    public void UnFreezeBall(params GameObject[] balls){
        foreach (GameObject item in balls)
        {
            if (item!=null)
            {
                item.GetComponent<Rigidbody2D>().isKinematic=false; 
                item.GetComponent<Rigidbody2D>().AddForce(saveSpeed,ForceMode2D.Impulse);
                               
            }
        }
    }

    void CreaPremio(){
        int aleatory= BallManager.bm.AleatoryNumber();

        if (aleatory==1)
        {
            Instantiate(premio, transform.position,Quaternion.identity);
        }
    }

}
