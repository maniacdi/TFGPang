using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelPoints : MonoBehaviour
{

    public Text ballDestroyed;
    int balls;
    public Text totalFruit;
    int fruits;
    public Text totalTime;
    int time;
    public Text totalScore;
    int points;
    public Text gameScore;

/*
*Se activa cuando el objeto se activa en la jeranquia.
*/
    private void OnEnable()
    {
        balls= GameManager.gm.ballDestroyed;
        ballDestroyed.text="x "+balls.ToString();

        fruits= GameManager.gm.fruitsCatched;
        totalFruit.text="x "+fruits.ToString();

        time=(int)GameManager.gm.time+1;
        totalTime.text=time.ToString() +" sec";

        SetTotalScore(ScoreManager.sm.points);

        StartCoroutine(TotalScoreAmount());


    }

    void SetTotalScore(int score){

        points += score;
        totalScore.text = points.ToString();
    }

    public IEnumerator TotalScoreAmount()
    {
        
        yield return new WaitForSeconds(1);

        while (balls>0)
        {
            balls--;
            SetTotalScore(100);
            ballDestroyed.text = "x "+balls.ToString();
            ScoreManager.sm.UpdateScore(100);
             yield return new WaitForSeconds(0.1f);
        }
        while (fruits>0)
        {
            fruits--;
            SetTotalScore(20);
            totalFruit.text = "x "+fruits.ToString();
            ScoreManager.sm.UpdateScore(20);
             yield return new WaitForSeconds(0.1f);
        }
         while (time>0)
        {
            time--;
            SetTotalScore(10);
            totalTime.text =time.ToString() +" sec";
            ScoreManager.sm.UpdateScore(10);
             yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1);
        if (SceneManager.sceneCountInBuildSettings>SceneManager.GetActiveScene().buildIndex+1)
        {
            GameManager.gm.NextLevel();          
        }
    }


}
