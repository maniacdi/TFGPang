using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager pm;
    public GameObject popText;

    private void Awake(){

        if(pm==null){
            pm=this;
        }else if(pm!=this){
            Destroy(gameObject);
        }
       
    }
    public void creaPopUpText(Vector2 startPos, int score){
        GameObject pop= Instantiate(popText);

        pop.transform.position= startPos;

        pop.GetComponent<TextMesh>().text=score.ToString();
   }
}