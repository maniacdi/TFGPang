using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerUps : MonoBehaviour
{
    public Sprite[] powerUps;
    public GameObject[] animatedPowerUps;

    private float boost=1.5f;

    SpriteRenderer sr;
    LifeManager lm;
    bool inGround;
    Player player;
    Player2 player2;
    private void Awake()
    
    {
        sr= GetComponent<SpriteRenderer>();
        lm= FindObjectOfType<LifeManager>();
        player=FindObjectOfType<Player>();
         if (SceneManager.GetActiveScene().name.Contains("player2_"))
        {
            player2=FindObjectOfType<Player2>();
        }
    }
    
    void Start()
    {
        //generar num aleatorio
        int alea= Random.Range(0,2);
        if (alea==0)
        {
            sr.sprite= powerUps[Random.Range(0, powerUps.Length)];
            gameObject.name=sr.sprite.name;
        }else{
            Instantiate(animatedPowerUps[Random.Range(0, animatedPowerUps.Length)], transform.position, Quaternion.identity);
            Destroy(gameObject);
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
            
            switch (gameObject.name)
            {
                case "DoubleArrow":
                    ShotManager.shm.ChangeShot(1);
                    break;
                case "Ancle":
                    ShotManager.shm.ChangeShot(2);
                    break;
                case "Laser":
                    ShotManager.shm.ChangeShot(3);
                    break;
                case "Piramyd":
                    ShotManager.shm.ChangeShot(4);
                    break;
                case "Reloj":
                    FreezeManager.fm.StartFreeze();    
                    break;    
                case "Vida":
                    lm.AmountLifes();    
                    break;        
                case "Sweet":
                    player.speedX= player.speedX*boost;
                    if (player2!=null)
                    {
                        player2.speedX= player2.speedX*boost;
                    }
                    break;
                }
            Destroy(gameObject);
        }
        
    }
   
}
