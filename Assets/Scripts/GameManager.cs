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
    public GameObject endScreen;
    public Slider senseSlider;
    public Slider aimAssistSlider;
    private DrawLines grappleManager;
    private string previousScreen;
    private ActionCameraController cameraController;
    public Toggle yAxisInvertButton;
    void Start()
    {
        resetGame();
        grappleManager = GameObject.Find("GrappleManager").GetComponent<DrawLines>();
        cameraController = GameObject.Find("Main Camera").GetComponent<ActionCameraController>();
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
        endScreen.SetActive(false);             
    }
    public void restartGame()
    {
        Cursor.visible = true;
        inGameUI.SetActive(false);
        startMenu.SetActive(true);
        controls.SetActive(false);
        pause.SetActive(false);
        endScreen.SetActive(false);  
        Scene scene = SceneManager.GetActiveScene();              
        SceneManager.LoadScene(scene.name);
    }
    public void startGame()
    {
        Cursor.visible = false;
        inGameUI.SetActive(true);
        startMenu.SetActive(false);
        controls.SetActive(false);
        pause.SetActive(false);
        endScreen.SetActive(false);               
        grappleManager.setGameStarted(true);
        Time.timeScale = 1.0f; 
    }
    public void showControlMenuFromStartMenu()
    {
        previousScreen = "start";
        inGameUI.SetActive(false);
        startMenu.SetActive(false);
        controls.SetActive(true);
        pause.SetActive(false);
        endScreen.SetActive(false);               
    }
    public void showControlMenuFromPauseMenu()
    {
        previousScreen = "pause";
        inGameUI.SetActive(false);
        startMenu.SetActive(false);
        controls.SetActive(true);
        pause.SetActive(false);              
        endScreen.SetActive(false);                
    }
    public void leaveControlMenu()
    {
        if(previousScreen.Equals("start"))
        {
            inGameUI.SetActive(false);
            startMenu.SetActive(true);
            controls.SetActive(false);
            pause.SetActive(false);
            endScreen.SetActive(false);  
        }
        else
        {
            inGameUI.SetActive(false);
            startMenu.SetActive(false);
            controls.SetActive(false);
            pause.SetActive(true);             
            endScreen.SetActive(false);  
        }
                  
    }
    public void setGamePaused()
    {
        Cursor.visible = true;
        inGameUI.SetActive(false);
        startMenu.SetActive(false);
        controls.SetActive(false);
        pause.SetActive(true);
        endScreen.SetActive(false);  
     
        // wherever actual game loop is
        grappleManager.setGameStarted(false);   
        Time.timeScale = 0.0f;
    }
    public void showEndScreen()
    {
        Cursor.visible = true;
        inGameUI.SetActive(false);
        startMenu.SetActive(false);
        controls.SetActive(false);
        pause.SetActive(false);
        endScreen.SetActive(true);
        grappleManager.setGameStarted(false);   
        Time.timeScale = 0.0f;
    }
    public void updateSensitivity()
    {
        cameraController.setSensitivity(senseSlider.value);
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void toggleYAxisInversion()
    {
        Debug.Log(yAxisInvertButton.isOn);
        cameraController.setYAxisInversion(yAxisInvertButton.isOn);
    }
    public void updateAimAssist()
    {
        grappleManager.setAimAssist(aimAssistSlider.value);
    }
}
