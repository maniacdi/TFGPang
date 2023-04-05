using UnityEngine;
using UnityEngine.SceneManagement;

public class PressStart : MonoBehaviour
{
    public GameObject pressStart;
    float time;

    void Start()
    {
        GameObject destroy=FindObjectOfType<DontDestroy>().gameObject;
        if (destroy!=null)
        {
            Destroy(destroy);
        }
    }
    
    void Update(){

        time+=Time.deltaTime;

        if(Mathf.RoundToInt(time)%2 ==0){

            pressStart.SetActive(true);

        }
        else{
            pressStart.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
