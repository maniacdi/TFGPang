using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallManager : MonoBehaviour
{
    public static BallManager bm;
    public List<GameObject> balls= new List<GameObject>();
    public bool destruyendo;
    Player player;
    Player2 player2;

    private void Awake(){

        if(bm==null){
            bm=this;
        }else if(bm!=this){
            Destroy(gameObject);
        }
        player=FindObjectOfType<Player>();
        if (SceneManager.GetActiveScene().name.Contains("player2_"))
        {
            player2=FindObjectOfType<Player2>();
        }
    }
    void Start()
    {
        //buscar bolas al comienzo y añadirlas
        balls.AddRange(GameObject.FindGameObjectsWithTag("Ball"));
        
    }
    
    // void Update()
    // {
    //     if (balls.Count==0)
    //     {
    //         player.Win();
    //         if (player2!=null)
    //         {
    //             player2.Win();
    //         }
    //         GameManager.inGame=false;
    //     }

       
    // }

    public void StartGame(){
        foreach (GameObject item in balls)
        {
            if (balls.IndexOf(item)%2==0)
            {
                item.GetComponent<Ball>().derch=true;

            }else{
                item.GetComponent<Ball>().derch=false;
            }
            item.GetComponent<Ball>().StartForce(item);
        }
    }

    public void Lose(){

        foreach (GameObject item in balls)
        {
            item.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
            item.GetComponent<Rigidbody2D>().isKinematic=true;
        }
    }

    public void DestroyBall(GameObject ball, GameObject b1, GameObject b2){
        Destroy(ball);
        balls.Remove(ball);
        balls.Add(b1);
        balls.Add(b2);
    }

    public void LastBall(GameObject ball){
         Destroy(ball);
        balls.Remove(ball);
    }

    public void Dinamita(int maxBalls){
        
        StartCoroutine(DinamitaBall(maxBalls));
    }

    List <GameObject> FindBalls(int typeBall){
        List <GameObject> ballsDestroy= new List <GameObject>();
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i].GetComponent<Ball>().name.Contains(typeBall.ToString()) && balls[i]!=null )
            {
                ballsDestroy.Add(balls[i]);
            }
        }
        return ballsDestroy;
    }

    void RecargaListaBolas(){
        balls.Clear();
         balls.AddRange(GameObject.FindGameObjectsWithTag("Ball"));
    }

    public IEnumerator DinamitaBall(int maxBalls)
    {
        RecargaListaBolas();
        destruyendo=true;
        int numAVer=1;
        while(numAVer<maxBalls){
            foreach (GameObject item in FindBalls(numAVer))
            {
                item.GetComponent<Ball>().Split();
                Destroy(item);
            }
            yield return new WaitForSeconds(0.3f);
            RecargaListaBolas();
            numAVer++;
        }
        destruyendo=false;
    }
    public int AleatoryNumber(){
        return Random.Range(0,3);
    }
}
