using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeManager : MonoBehaviour
{
    public static FreezeManager fm;
    public Text freezeTimeText;
    public GameObject freezeTimeCount;
    public float time;
    public bool freeze;
    private void Awake(){

        if(fm==null){
            fm=this;
        }else if(fm!=this){
            Destroy(gameObject);
        }
    }
    void Start()
    {
        freezeTimeCount.SetActive(false);
    }

    void Update()
    {
        
    }
    public void StartFreeze(){
        time=3;
        if (!freeze)
        {
            StartCoroutine(FreezeTime());
        }
    }
    public IEnumerator FreezeTime(){
        freeze=true;
        foreach (GameObject item in BallManager.bm.balls)
        {
             if (item != null)
            {
                item.GetComponent<Ball>().FreezeBall(item);
            }
        }
        freezeTimeCount.SetActive(true);

        while (time>0)
        {
            time -= Time.deltaTime;
            freezeTimeText.text=time.ToString("f2");
            yield return null;
        }
        freezeTimeCount.SetActive(false);
        time=0;
        foreach (GameObject item in BallManager.bm.balls)
        {
            if (item != null)
            {
                item.GetComponent<Ball>().UnFreezeBall(item);
            }
            
        }
        
        freeze=false;
        if (GameManager.gm.Modo()==GameMode.PANIC)
        {
            StartCoroutine(BallSpawn.bs.MoveDown());
        }
        
    }
}
