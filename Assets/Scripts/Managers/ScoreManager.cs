using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager sm;
    public Text scoreText;
    public int points=0;
    public Text hiScoreText;
    public int hiScore=0;


    private void Awake(){

        if(sm==null){
            sm=this;
        }else if(sm!=this){
            Destroy(gameObject);
        }
    }
    void Start()
    {
        points=0;
        scoreText.text=points.ToString();
        
    }

    public void UpdateScore(int pts){
        points+=pts;
        scoreText.text =points.ToString();

        if (points>hiScore)
        {
            hiScore=points;
            hiScoreText.text="HS-"+hiScore.ToString();
            PlayerPrefs.SetInt("HiScore",hiScore);
        }
    }
    /**
    *Cargae el fichero con el valor del hiscore almacenado, pa tenr los datos
    */
    public void UpdateHiScore(){
        hiScore=PlayerPrefs.GetInt("HiScore");
        hiScoreText.text="HS-"+hiScore.ToString();

    }
   
}
