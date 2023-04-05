using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHS : MonoBehaviour
{
    public Text[] hsText;
    HighScores hsm;
    void Start()
    {
        for(int i=0; i< hsText.Length;i++){
            hsText[i].text= i+1+". ";
        }
        hsm=GetComponent<HighScores>();

        StartCoroutine(RefreshHS());
    }

    public void HSDownloaded(HighScore[] list){
        for (int i = 0; i < hsText.Length; i++)
        {
            hsText[i].text = i+1 +". ";
            if (list.Length>i)
            {
                hsText[i].text += list[i].user +" - "+list[i].score;
            }
        }
    }

    IEnumerator RefreshHS(){
        while(true){
            hsm.DownloadHS();
            yield return new WaitForSeconds(100);
        }
    }



}
