using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    
    public float speedX = 4.0f;
    float speedY = 4.0f;
    float movementX= 0;
    float movementY= 0;
    float climbY=0;
    float newY;
    bool rightWall;
    bool leftWall;
    public GameObject shield;
    public bool blink;

    LifeManager lm;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;

    public bool climb;
    float defaultPosY;
    public bool inGround;
    public bool isUp;

    public KeyCode up;
    public KeyCode left;
    public KeyCode right;
    public KeyCode down;

    Player2 player2;
    

    private void Awake(){
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        lm= FindObjectOfType<LifeManager>();
    }
    void Start() {
        defaultPosY=transform.position.y;
         if (SceneManager.GetActiveScene().name.Contains("player2_"))
        {
            player2=FindObjectOfType<Player2>();
        }
    }
    
    void Update()
    {
        if (GameManager.inGame)
        {
            movementX=Input.GetAxisRaw("Horizontal")* speedX;
            movementY=Input.GetAxisRaw("Vertical")* speedY;
            animator.SetInteger("velX", Mathf.RoundToInt(movementX));
            animator.SetInteger("velY", Mathf.RoundToInt(movementY));
            if(movementX<0){
                sr.flipX=true;
            }else{
                sr.flipX = false;
            }
        }
    }
    //para los movimientos
    private void FixedUpdate(){

        if (GameManager.inGame)
        {
            if(leftWall){
                if (Input.GetKey(left))
                {
                    speedX=0;               
                }else if (Input.GetKey(right)|| Input.GetKeyUp(left))
                {
                    speedX=4;
                }
            }
            if(rightWall){
                if (Input.GetKey(right))
                {
                    speedX=0;               
                }else if (Input.GetKey(left)|| Input.GetKeyUp(right))
                {
                    speedX=4;
                }
            }
            
            if (transform.position.y>=climbY)
            {
                isUp=true;
            }else
            {
                isUp=false;
            }
            if (climb)
            {
                if ((Input.GetKey(up)&& !isUp)|| (Input.GetKey(down)&&!inGround))
                {
                    speedY=4;
                }else
                {
                    speedY=0;
                }
            }else
            {
                speedY=0;
            }
            if (movementX!=0)
            {
                rb.MovePosition(rb.position + Vector2.right * movementX * Time.fixedDeltaTime);
            }else if((transform.position.y>= defaultPosY && climb && !isUp)||(isUp &&Input.GetKey(down))){
                rb.MovePosition(rb.position + Vector2.up * movementY * Time.fixedDeltaTime);

            }
        }
        newY=Mathf.Clamp(transform.position.y, -2.3f, 10f);
        transform.position = new Vector2( transform.position.x,newY);
        if (!inGround && !climb)
        {
            transform.position += new Vector3(movementX/5,-1)*Time.deltaTime*3;
        }
    
    }

    public void Win(){

        shield.SetActive(false);
        animator.SetBool("win", true);

    }

    private void OnBecameInvisible()
    {
        if (lm.lifes<=0)
        {
            GameManager.gm.StartGameOver();
        }else{
            Invoke("ReloadLevel", 0.5f); 
        }
       
    }

    void ReloadLevel(){
        lm.SubLifes();
        lm.RestartDoll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.inGame && !FreezeManager.fm.freeze)
        {
                    
            if (other.gameObject.tag=="Ball")
            {
                if (shield.activeInHierarchy)
                {
                    shield.SetActive(false);
                    StartCoroutine(Parpadeando());
                }else{
                    if(!blink){
                        StartCoroutine(Lose());
                        if (player2!=null)
                        {
                            StartCoroutine(player2.Lose());
                        }                        
                    }
                }
            }
            if (!GameManager.inGame && (other.gameObject.tag=="Left"||other.gameObject.tag=="Right"))
            {
                sr.flipX=!sr.flipX;
                rb.velocity /=3;
                rb.velocity *=1;
                rb.AddForce(Vector3.up*5, ForceMode2D.Impulse);

            }
        }
        if (other.gameObject.tag=="Ladder")
        {
            if (!isUp)
            {
                climbY=transform.position.y+other.GetComponent<BoxCollider2D>().size.y -0.2f;
            }
        }
    }

    //cuando sigue en contacto con el collider
   private void OnTriggerStay2D(Collider2D collision)
   {
        if(collision.gameObject.tag=="Left"){
            leftWall=true;
        }else if (collision.gameObject.tag=="Right")
        {
            rightWall=true;
        }

        if (collision.gameObject.tag=="Ladder")
        {
            climb=true;
        }
        if (collision.gameObject.tag=="Ground" ||collision.gameObject.tag=="Platform"
        && transform.position.y > collision.gameObject.transform.position.y)
        {
            inGround=true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){

         if(collision.gameObject.tag=="Left"){
            leftWall=false;
        }else if (collision.gameObject.tag=="Right")
        {
            rightWall=false;
        }
        if (collision.gameObject.tag=="Ladder")
        {
            climb=false;
        }
        if (collision.gameObject.tag=="Ground" ||collision.gameObject.tag=="Platform")
        {
            inGround=false;
        }
    }
    public IEnumerator Parpadeando()
    {
        blink=true;
        for (int i = 0; i < 8; i++)
        {
            if (blink && GameManager.inGame)
            {
                sr.color=new Color(1,1,1,0);
                yield return new WaitForSeconds(0.2f);
                sr.color=new Color(1,1,1,1);
                yield return new WaitForSeconds(0.2f);
            }else{
                break;
            }
        }
        blink=false;
    }

    public IEnumerator Lose()
    {
        GameManager.inGame=false;
        animator.SetBool("lose",true);
        BallManager.bm.Lose();
        lm.LifeLose();
        yield return new WaitForSeconds(1);
        rb.isKinematic=false;

        if (transform.position.x<0)
        {
            rb.AddForce(new Vector2(-10,10), ForceMode2D.Impulse);
        }else{
            rb.AddForce(new Vector2(10,10), ForceMode2D.Impulse);
        }
    }
  
}
