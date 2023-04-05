using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SelectMode : MonoBehaviour
{
    public Image onePlayerImage;
    public Text onePlayerText;
    public Image twoPlayerImage;
    public Text twoPlayerText;

    public bool one;

    void Start()
    {
        one=true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace)){
           SceneManager.LoadScene("selectGame");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        if (one)
        {
            
            onePlayerImage.color=new Color(1,1,1);
            onePlayerText.color=new Color(1,1,1);
        
            twoPlayerImage.color=new Color(1,1,0,0.5f);
            twoPlayerText.color=new Color(1,1,0,0.5f);

            if (Input.GetKeyDown(KeyCode.RightArrow)){
                one=false;
            }
            if (Input.GetKeyDown(KeyCode.Return)){
                SceneManager.LoadScene("player1_01");
            }
                
        }else{

            twoPlayerImage.color=new Color(1,1,1);
            twoPlayerText.color=new Color(1,1,1);
        
            onePlayerImage.color=new Color(1,1,0,0.5f);
            onePlayerText.color=new Color(1,1,0,0.5f);
            
            if (Input.GetKeyDown(KeyCode.LeftArrow)){
                one=true;
            }
            if (Input.GetKeyDown(KeyCode.Return)){
                SceneManager.LoadScene("player2_01");
            }
        }
    }
}
