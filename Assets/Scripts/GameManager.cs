using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inGameUI;
    public GameObject startMenu;
    public GameObject controls;
    public GameObject pause;
    public Slider senseSlider;
    private GameObject grappleManager;
    void Start()
    {
        resetGame();
        grappleManager = GameObject.Find("GrappleManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && grappleManager.GetComponent<DrawLines>().getGameStarted())
        {
            setGamePaused();
        }
    }

    public void resetGame()
    {
        inGameUI.SetActive(false);
        startMenu.SetActive(true);
        controls.SetActive(false);
        pause.SetActive(false);            
    }
    public void restartGame()
    {
        inGameUI.SetActive(false);
        startMenu.SetActive(true);
        controls.SetActive(false);
        pause.SetActive(false);              
        SceneManager.LoadScene("SampleScene");
    }
    public void startGame()
    {
        inGameUI.SetActive(true);
        startMenu.SetActive(false);
        controls.SetActive(false);
        pause.SetActive(false);              
        // wherever actual game loop is
        grappleManager.GetComponent<DrawLines>().setGameStarted(true);
        Time.timeScale = 1.0f; 
    }
    public void showControlMenu()
    {
        inGameUI.SetActive(false);
        startMenu.SetActive(false);
        controls.SetActive(true);
        pause.SetActive(false);              
    }
    public void leaveControlMenu()
    {
        inGameUI.SetActive(false);
        startMenu.SetActive(true);
        controls.SetActive(false);
        pause.SetActive(false);                    
    }
    public void setGamePaused()
    {
        inGameUI.SetActive(false);
        startMenu.SetActive(false);
        controls.SetActive(false);
        pause.SetActive(true);          
        // wherever actual game loop is
        grappleManager.GetComponent<DrawLines>().setGameStarted(false);   
        Time.timeScale = 0.0f;
    }
    public void updateSensitivity()
    {
        GameObject.Find("Main Camera").GetComponent<ActionCameraController>().setSensitivity(senseSlider.value);   
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
