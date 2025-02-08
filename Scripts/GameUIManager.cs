/**One of the two main Scripts.
 * This Script handles most of the UI and does Scene Management*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;                //Object for pausing
    [SerializeField] private GameObject endScreen;              //Object for endscreen
    [SerializeField] private HighScoreDisplay scoreDisplay;     //Final Score Display
    //[SerializeField] private Logic logic;                       //Object for Logic class
    public void OnHome()
    {
        //SceneManager.LoadScene("MainGame");         //Will load main Game
    }
    public void OnPlay()
    {
        pauseUI.SetActive(false);       //For making the Options Panel(Pause Panel) invisible
        endScreen.SetActive(false);     //For making Endscreen invisible
        Time.timeScale = 1.0f;          //For resuming the game
    }
    public void OnTryAgain()
    {
        //logic.Reset();            //Resetting the values in logic
        pauseUI.SetActive(false);       
        endScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);     //Restarting the current scene
    }
    public void OnPause()
    {
        pauseUI.SetActive(true);    //Displaying the Pause Panel
        Time.timeScale = 0f;        //Stopping Game
    }

    private void Update()
    {
        float currentTime = Time.timeSinceLevelLoad;     //Find time from logic
        if (currentTime >= 60)                      //Game Duration
        {
            scoreDisplay.ScoreDisplay();            //Display Score
            Time.timeScale = 0f;                    //Pause Game
            endScreen.SetActive(true);              //Display Endscreen
        }
    }
}
