using UnityEngine;

public class ShotGun : MonoBehaviour
{
    float speed=8;

    void Update()
    {
        if (transform.rotation.z==0)
        {
            transform.position += Vector3.up*Time.deltaTime*speed;
        }else if (transform.rotation.z<0)
        {
            transform.position += new Vector3(0.1f,1,0)*Time.deltaTime*speed;
        }else{
            transform.position += new Vector3(-0.1f,1,0)*Time.deltaTime*speed;
        }

    }
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
