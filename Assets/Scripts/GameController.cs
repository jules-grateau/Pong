using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Score
    private int scoreP1 = 0;
    private int scoreP2 = 0;
    public int winScore = 10;
    #endregion

    #region Game State
    public bool isPaused { get; set; } = true;
    public bool isGameStarted { get; set; } = false;
    public bool inSettings { get; set; } = false;
    public float waitTimeCountdown { get; set; } = 0.5f;
    #endregion

    #region UI
    //Score
    public Text scoreP1Text;
    public Text scoreP2Text;

    //Menu
    public GameObject MenuUI;
    public Text StartButtonText;
    public Button ResumeButton;

    //Countdown
    public GameObject CountdownContainer;
    public Text CountdownText;

    //WinScreen
    public GameObject WinScreenContainer;
    public Text WinText;

    //Settings
    public GameObject SettingsContainer;
    #endregion


    public GameObject ballPrefab;
    public GameObject BallInstance { get; set; }


    public PlayerController playerController;
    public ComputerController computerController;
    private SettingsManager settingsManager;



    public void Start()
    {
        initGameState();
        MenuUI.SetActive(true);
        settingsManager = GetComponent<SettingsManager>();
    }
    public void Update()
    {
        if(!inSettings && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameStarted && !isPaused)
            {
                OnPause();
                return;
            } 
            if (isGameStarted && isPaused)
            {
                OnResume();
                return;
            }
                
        }
    }

    #region Button Actions
    public void OnStart()
    {
        Reset();
        BallInstance = Instantiate(ballPrefab);
        MenuUI.SetActive(false);
        isGameStarted = true;
        StartButtonText.text = "Restart Game";
        ResumeButton.interactable = true;
        StartCoroutine(Countdown());
    }
    public void OnPause()
    {
        PauseGameState();
        MenuUI.SetActive(true);
    }

    public void OnResume()
    {
        MenuUI.SetActive(false);
        StartCoroutine(Countdown());
    }

    public void OnReturnMenu()
    {
        WinScreenContainer.SetActive(false);
        initGameState();
        MenuUI.SetActive(true);
    }

    public void OnRestart()
    {
        initGameState();
        Reset();
        WinScreenContainer.SetActive(false);
        OnStart();
    }

    public void OnOpenSettings()
    {
        SettingsContainer.SetActive(true);
        inSettings = true;
    }

    public void OnCloseSettings()
    {
        SettingsContainer.SetActive(false);
        inSettings = false;
    }

    #endregion

    #region Game State Manipulation 
    private void PauseGameState()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    private void UnpauseGameState()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    private void initGameState()
    {
        Time.timeScale = 0;
        isPaused = true;
        isGameStarted = false;
        StartButtonText.text = "Start Game";
        WinScreenContainer.SetActive(false);
        SettingsContainer.SetActive(false);
        ResumeButton.interactable = false;
    }
    private void Reset()
    {
        if (BallInstance != null)
            Destroy(BallInstance);

        scoreP1 = 0;
        scoreP2 = 0;
        UpdateScoreUI();
        playerController.ResetPosition();
        computerController.ResetPosition();
    }

    public void ScoreGoal(int player)
    {
        if (player == 1)
        {
            scoreP1++;

        }

        if (player == 2)
        {
            scoreP2++;

        }

        UpdateScoreUI();
        if (scoreP1 >= settingsManager.winScore)
        {
            Win(1);
            return;
        }
        if (scoreP2 >= settingsManager.winScore)
        {
            Win(2);
            return;
        }

        StartCoroutine(SpawnBall());
    }

    private IEnumerator SpawnBall()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        BallInstance = Instantiate(ballPrefab);
    }

    #endregion

    #region UI Manipulation 

    private void UpdateScoreUI()
    {
        scoreP1Text.text = scoreP1.ToString();
        scoreP2Text.text = scoreP2.ToString();
    }
    private IEnumerator Countdown()
    {
        CountdownText.text = "3";
        CountdownContainer.SetActive(true);
        yield return new WaitForSecondsRealtime(waitTimeCountdown);
        CountdownText.text = "2";
        yield return new WaitForSecondsRealtime(waitTimeCountdown);
        CountdownText.text = "1";
        yield return new WaitForSecondsRealtime(waitTimeCountdown);
        CountdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(waitTimeCountdown);
        CountdownContainer.SetActive(false);
        UnpauseGameState();
    }

    private void Win(int playerNumber)
    {
        Color color;
        PauseGameState();
        if (playerNumber == 1)
        {
            WinText.text = "You won !";
            if (ColorUtility.TryParseHtmlString("#fbc531", out color))
                WinText.color = color;
        }
        else
        {
            WinText.text = "You lost !";
            if (ColorUtility.TryParseHtmlString("#c23616", out color))
                WinText.color = color;
        }
        
        WinScreenContainer.SetActive(true);
    }

    #endregion




}
