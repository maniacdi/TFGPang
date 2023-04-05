using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeShotImage : MonoBehaviour
{
    public GameObject arrowShot;
    public GameObject ancleShot;
    public GameObject laserShot;
    public GameObject jeringShot;
    public GameObject piramydShot;
    
    public void TypeShot(string shot){
        if (shot.Equals("Arrow"))
        {
            arrowShot.SetActive(true);
            ancleShot.SetActive(false);  
            laserShot.SetActive(false); 
            jeringShot.SetActive(false); 
            piramydShot.SetActive(false);

        }else if(shot.Equals("Ancle"))
        {
            arrowShot.SetActive(false);
            ancleShot.SetActive(true);  
            laserShot.SetActive(false); 
            jeringShot.SetActive(false); 
            piramydShot.SetActive(false);
        }
        else if(shot.Equals("Laser"))
        {
            arrowShot.SetActive(false);
            ancleShot.SetActive(false);  
            laserShot.SetActive(true); 
            jeringShot.SetActive(false); 
            piramydShot.SetActive(false);

        }else if(shot.Equals("Piramyd"))
        {
            arrowShot.SetActive(false);
            ancleShot.SetActive(false);  
            laserShot.SetActive(false); 
            jeringShot.SetActive(false); 
            piramydShot.SetActive(true);

        }else{
             arrowShot.SetActive(false);
            ancleShot.SetActive(false);  
            laserShot.SetActive(false); 
            jeringShot.SetActive(true); 
            piramydShot.SetActive(false);
        }
    }
}
