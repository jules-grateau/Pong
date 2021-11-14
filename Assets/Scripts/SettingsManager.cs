using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    #region UI
    public Text winScoreText;
    public Slider winScoreSlider;

    public Text computerSpeedText;
    public Slider computerSpeedSlider;

    public Text playerSpeedText;
    public Slider playerSpeedSlider;
    #endregion
    private const string WinScoreKey = "WinScore";
    public int winScore { get; set; } = 10;

    private const string ComputerSpeedKey = "ComputerSpeed";
    public float computerSpeed { get; set; } = 3f;

    private const string PlayerSpeedKey = "PlayerSpeed";
    public float playerSpeed { get; set; } = 4f;

    private void Start()
    {

        if (PlayerPrefs.HasKey(WinScoreKey))
            winScore = PlayerPrefs.GetInt(WinScoreKey);
        if (PlayerPrefs.HasKey(ComputerSpeedKey))
            computerSpeed = PlayerPrefs.GetFloat(ComputerSpeedKey);
        if (PlayerPrefs.HasKey(PlayerSpeedKey))
            playerSpeed = PlayerPrefs.GetFloat(PlayerSpeedKey);

        winScoreText.text = winScore.ToString();
        winScoreSlider.SetValueWithoutNotify(winScore);

        computerSpeedText.text = computerSpeed.ToString();
        computerSpeedSlider.SetValueWithoutNotify(computerSpeed);

        playerSpeedText.text = playerSpeed.ToString();
        playerSpeedSlider.SetValueWithoutNotify(playerSpeed);
    }

    public void OnChangeWinScore()
    {
        winScoreText.text = winScoreSlider.value.ToString();
    }
    public void OnChangeComputerSpeed()
    {
        computerSpeedText.text = computerSpeedSlider.value.ToString();
    }

    public void OnChangePlayerSpeed()
    {
        playerSpeedText.text = playerSpeedSlider.value.ToString();
    }

    public void OnApplySettings()
    {
        winScore = (int) winScoreSlider.value;
        PlayerPrefs.SetInt(WinScoreKey, winScore);

        computerSpeed = computerSpeedSlider.value;
        PlayerPrefs.SetFloat(ComputerSpeedKey,computerSpeed);

        playerSpeed = playerSpeedSlider.value;
        PlayerPrefs.SetFloat(PlayerSpeedKey, playerSpeed);
    }
}
