using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int lifes=3;
    public Text lifesText;
    public GameObject doll;
    Animator animator;

    private void Awake(){
        animator= doll.GetComponent<Animator>();
    }
   
    public void AmountLifes()
    {
        lifes++;
        UpdateLifes();
    }
    public void SubLifes(){
        lifes--;
        UpdateLifes();
    }
    public void UpdateLifes(){
        lifesText.text="x "+lifes.ToString();
    }

    public void LifeWin(){
        animator.SetBool("win",true);
    }

    public void LifeLose(){
        animator.SetBool("lose",true);
    }
    
    public void RestartDoll(){
        animator.SetBool("win",false);
        animator.SetBool("lose",false);
    }

}
