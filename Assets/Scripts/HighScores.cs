using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class HighScores : MonoBehaviour
{
    const string privateCode="x58ck7I6mk2QSjIgVrG8Fgp5SrTc2UGket-GpKRPEcVA";
    const string publicCode="600d4f58778d3d1684915131";
    const string webURL="http://dreamlo.com/lb/";
    public HighScore[] hsList;
    static HighScores instance;
    DisplayHS hsDisplay;

    void Awake()
    {
        instance=this;
        hsDisplay=GetComponent<DisplayHS>();    
        DownloadHS();
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace)){
                SceneManager.LoadScene("main");
            }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public static void AddNewHS(string user, int score){
        instance.StartCoroutine(instance.UploadNewHS(user,score));
    }

    IEnumerator UploadNewHS(string user, int score){
        UnityWebRequest  www = new UnityWebRequest(webURL + privateCode +"/add/" + user+"/"+score);
         print(user);
          print(score);
        yield return www.SendWebRequest();
        if(www.isNetworkError) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log("Upload complete!");
            DownloadHS();
        }

    }
    public void DownloadHS(){
        StartCoroutine(DownloadHSFromDB());
    }

    IEnumerator DownloadHSFromDB(){

         UnityWebRequest www = new UnityWebRequest(webURL + publicCode +"/pipe/0/10");
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();
 
        if(www.isNetworkError) {
            Debug.Log(www.error);
        }
        else {
           
            FormatHS(www.downloadHandler.text);
            hsDisplay.HSDownloaded(hsList);
        }
    }

    void FormatHS(string text){
        string[] entradas= text.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        hsList= new HighScore[entradas.Length];
        for (int i = 0; i < entradas.Length; i++)
        {
            string[] info = entradas[i].Split(new char []{'|'});
            string user= info[0];
            int score= int.Parse(info[1]);
            hsList[i]= new HighScore(user,score);
            //print(hsList[i].user+ " "+hsList[i].score);
        }
    }
}
public struct HighScore{
    public string user;
    public int score;
    public HighScore(string user,int score){
        this.user=user;
        this.score=score;
    }
}
