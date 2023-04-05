using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiramydShot : MonoBehaviour
{
  
    void OnTriggerEnter2D(Collider2D collider)
    {
         if (collider.gameObject.tag=="Ball")
        {
            collider.gameObject.GetComponent<Ball>().Split();
        }
          if (collider.gameObject.tag!="Player" || collider.gameObject.tag!="Ladder")
        {
            Destroy(gameObject);
            ShotManager.shm.DestroyShot();
        }
    }
}
