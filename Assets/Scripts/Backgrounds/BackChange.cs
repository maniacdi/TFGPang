using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackChange : MonoBehaviour
{

    public Sprite[] backGrounds;
    Image currentBack;
    void Start()
    {
        currentBack= GetComponent<Image>();
        currentBack.sprite=backGrounds[Random.Range(0,backGrounds.Length)];
    }

    
}
