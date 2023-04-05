using UnityEngine;
using UnityEngine.SceneManagement;

public class ShotManager : MonoBehaviour
{
    public static ShotManager shm;
    public GameObject[] Shots;
    Transform player;
    Transform player2;

    public int maxShots;
    public int numShots=0;

    //como una maquina de estado
    public int typeOfShot; //0-arrow, 1-doble, 2-ancla, 3 laser

    public KeyCode shot1;
    public KeyCode shot2;

    Animator animator;

    TypeShotImage shotImage;

    private void Awake(){

        if(shm==null){
            shm=this;
        }else if(shm!=this){
            Destroy(gameObject);
        }
       player= FindObjectOfType<Player>().transform;
        if (SceneManager.GetActiveScene().name.Contains("player2_"))
        {
            player2=FindObjectOfType<Player2>().transform;
        }
       shotImage=FindObjectOfType<TypeShotImage>();
    }
    void Start()
    {
        typeOfShot=0;
         if(player2!=null){
            maxShots=2;
        }else{
            maxShots=1;
         }
        
    }

    void Update()
    {
        if(CanShot()&& Input.GetKeyDown(shot1) && GameManager.inGame){
            Shot();
        }
        if(CanShot()&& Input.GetKeyDown(shot2) && GameManager.inGame){
            Shot2();
        }
        if (numShots ==maxShots && GameObject.FindGameObjectsWithTag("Arrow").Length==0
            && GameObject.FindGameObjectsWithTag("Ancle").Length==0)
        {
            numShots=0;
        }
    }
    bool CanShot(){
        if (numShots < maxShots)
        {
            return true;
        }
        return false;
    }

    void Shot(){
        if (typeOfShot!=3)
        {
            Instantiate(Shots[typeOfShot], player.position, Quaternion.identity);           

        }else{
            Instantiate(Shots[3], new Vector2(player.position.x + 0.5f, player.position.y+1), Quaternion.Euler(new Vector3(0,0,-5)));
            Instantiate(Shots[3], new Vector2(player.position.x, player.position.y+1), Quaternion.identity);
            Instantiate(Shots[3], new Vector2(player.position.x - 0.5f, player.position.y+1), Quaternion.Euler(new Vector3(0,0,5)));            
        }
        numShots++; 
    }
        void Shot2(){
        if (typeOfShot!=3)
        {
           
            Instantiate(Shots[typeOfShot], player2.position, Quaternion.identity);

        }else{
            Instantiate(Shots[3], new Vector2(player2.position.x + 0.5f, player2.position.y+1), Quaternion.Euler(new Vector3(0,0,-5)));
            Instantiate(Shots[3], new Vector2(player2.position.x, player2.position.y+1), Quaternion.identity);
            Instantiate(Shots[3], new Vector2(player2.position.x - 0.5f, player2.position.y+1), Quaternion.Euler(new Vector3(0,0,5)));

        }
        numShots++; 
    }
    public void DestroyShot(){
        if (numShots>0 && numShots<maxShots)
        {
            numShots--;
        }        
    }
    public void ChangeShot(int type){

        if (typeOfShot!=type)
        {
            switch (type)
            {
                case 0:
                    if(player2!=null){
                        maxShots=2;
                    }else{
                        maxShots=1;
                    }
                    
                    shotImage.TypeShot("");
                    break;
                case 1:
                     if(player2!=null){
                        maxShots=4;
                    }else{
                        maxShots=2;
                    }
                    shotImage.TypeShot("Arrow");
                    break;
                case 2:
                     if(player2!=null){
                        maxShots=2;
                    }else{
                        maxShots=1;
                    }
                     shotImage.TypeShot("Ancle");
                    break;
                case 3:
                    if(player2!=null){
                        maxShots=16;
                    }else{
                        maxShots=8;
                    }
                  
                     shotImage.TypeShot("Laser");
                    break;
                case 4:
                 if(player2!=null){
                        maxShots=16;
                    }else{
                        maxShots=8;
                    }
                  
                     shotImage.TypeShot("Piramyd");
                    break;
            }
            typeOfShot=type;
            numShots=0;
        }

    }
}
