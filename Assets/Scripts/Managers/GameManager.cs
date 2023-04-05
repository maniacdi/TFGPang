using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameMode{PANIC, TOUR};

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public static bool inGame;
    public GameObject ready;
    public GameObject gameOver;

    public GameMode gameMode;

    public Text timeText;
    public float time=100;

    Player player;
    public Player2 player2;
    LifeManager lm;
    Fruits fruits;
    public GameObject panel;
    public GameObject panelPausa;
    PanelPoints panelPoints;
    public bool activo=false;

    public int ballDestroyed=0;
    public int fruitsCatched=0;

    Image progressBar;
    Text levelText;
    public int currentLevel=1;

    Animator animator;

    private void Awake(){

        if(gm==null){
            gm=this;
        }else if(gm!=this){
            Destroy(gameObject);
        }
        player= FindObjectOfType<Player>();
         if (SceneManager.GetActiveScene().name.Contains("player2_"))
        {
            player2=FindObjectOfType<Player2>();
        }
        lm= FindObjectOfType<LifeManager>();
        fruits= FindObjectOfType<Fruits>();

        if (SceneManager.GetActiveScene().name.Equals("Panic"))
        {
            gameMode=GameMode.PANIC;
        }else{
            gameMode=GameMode.TOUR;
        }

    }
    
    void Start()
    {
        StartCoroutine(GameStart());
        ScoreManager.sm.UpdateHiScore();
        gameOver.SetActive(false);
        if (gameMode==GameMode.PANIC)
        {
            progressBar = GameObject.FindGameObjectWithTag("Progress").GetComponent<Image>();
            levelText = GameObject.FindGameObjectWithTag("Level").GetComponent<Text>();
            progressBar.fillAmount=0;
        }
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("main");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            activo = !activo;
            panelPausa.SetActive(activo);
            Time.timeScale = (GameManager.gm.activo) ? 0 : 1f;
        }
        if (gameMode==GameMode.TOUR){

            if(BallManager.bm.balls.Count==0){
                inGame=false;
                if (player2!=null){
                    player2.Win();
                }
                player.Win();
                lm.LifeWin();
                panel.SetActive(true);
                panelPoints=panel.GetComponent<PanelPoints>();
            }
            if (inGame){
                time-=Time.deltaTime;
                timeText.text= "TIME " + time.ToString("f0");
            }
        }else{
             if(BallManager.bm.balls.Count==0 && BallSpawn.bs.free){
                 BallSpawn.bs.NextBall();
             }
        }
       
    }

    public void UpdateBalls(){

        ballDestroyed++;
        if (ballDestroyed % Random.Range(3,15)==0 && BallManager.bm.balls.Count>0)
        {
            fruits.CreateFruit();    
        }
    }

    public void NextLevel(){
        lm.RestartDoll();
       
        if (SceneManager.GetActiveScene().name.Contains("player1_") &&
        !SceneManager.GetActiveScene().name.Equals("player1_03"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
        
        if (SceneManager.GetActiveScene().name.Contains("player2_") &&
        !SceneManager.GetActiveScene().name.Equals("player2_03"))
        {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }

        if (SceneManager.GetActiveScene().name.Contains("_03"))
        {
             SceneManager.LoadScene(0);
        }
    }
    public void StartGameOver(){
        if(gm!=null){
            StartCoroutine(GameOver());
        }
    }
    public IEnumerator GameStart(){

        yield return new WaitForSeconds(2);
        ready.SetActive(false);

        if (gameMode==GameMode.TOUR){
            BallManager.bm.StartGame();
        }else{
            BallSpawn.bs.NextBall();
        }
        inGame=true;
    
    }
    public IEnumerator GameOver(){

        gameOver.SetActive(true);
        yield return new WaitForSeconds(5);
        
        if (gameMode==GameMode.PANIC)
        {
            /*activar panel,
            introducir nombre y que sea el user
            coger la puntuacion

            */
        }
        
        SceneManager.LoadScene(0);

    
    }

    public void PanicProgress(){
        if (gameMode==GameMode.PANIC)
        {
            progressBar.fillAmount += 0.1f;
            if ( progressBar.fillAmount==1)
            {
                 progressBar.fillAmount=0;
                 currentLevel++;
                 BallSpawn.bs.IncreaseDificulty();
                 if (currentLevel<10)
                 {
                     levelText.text="Level 0"+currentLevel.ToString();
                 }else{
                     levelText.text="Level "+currentLevel.ToString();
                 }
            }
        }
    }
    public GameMode Modo(){
        return gameMode;
    }

  
}
