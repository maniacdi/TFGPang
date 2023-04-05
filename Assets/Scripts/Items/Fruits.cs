using UnityEngine;

public class Fruits : MonoBehaviour
{
    
    public GameObject fruit;

    public void CreateFruit(){
        Instantiate(fruit, transform.position, Quaternion.identity);
    }

    
}
