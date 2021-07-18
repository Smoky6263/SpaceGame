using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuScript : MonoBehaviour
{

    [SerializeField] private GameObject gameMenuObj, gameSettingsObj;
    [SerializeField] private Button resumeButton;
    [SerializeField] private TMP_Text playerControlText;


    public delegate void ControlChanged_Delegate(bool value);
    public static event ControlChanged_Delegate controlChanged_Event;

    private PlayerScript playerScript;

    private bool gameStarted = false, gameUnPaused = true, isKeyboardControl = false;

    private void Start()
    {
        gameStarted = false;
        PlayerControlChanged();
        Time.timeScale = 0f;
        playerScript = Transform.FindObjectOfType<PlayerScript>();
        playerScript.enabled = false;
    }    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            gameMenuObj.gameObject.SetActive(gameUnPaused);
            Time.timeScale = gameUnPaused ? 0f : 1f;
            gameUnPaused = !gameUnPaused;
            playerScript.enabled = gameUnPaused;
            gameSettingsObj.SetActive(false);
        }
    }

    public void ResumeButtonPressed()
    {
        gameMenuObj.gameObject.SetActive(gameUnPaused);
        Time.timeScale = gameUnPaused ? 0f : 1f;
        gameUnPaused = !gameUnPaused;
        playerScript.enabled = gameUnPaused;

    }

    public void NewGameButtonPressed()
    {
        resumeButton.interactable = true;
        gameMenuObj.gameObject.SetActive(false);
        playerScript.enabled = true;


        if (gameStarted)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        gameStarted = true;
        Time.timeScale = 1f;
    }

    public void PlayerControlChanged()
    {
        isKeyboardControl = !isKeyboardControl;
        controlChanged_Event(isKeyboardControl);

        if (isKeyboardControl)
            playerControlText.text = "Control: Keyboard";
        else
            playerControlText.text = "Control: Keyboard and Mouse";


    }

    public void ExitGameButtonPressed()
    {
        Application.Quit();
    }
}
