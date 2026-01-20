using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Health & Energy Bars")]
    public Slider healthBar;
    public Slider energyBar;

    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject startMenu;

    private bool isPaused = false;

    void Start()
    {
        // Hide menus at start
        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (startMenu != null) startMenu.SetActive(true);
    }

    public void UpdateHealth(float current, float max)
    {
        healthBar.value = current / max;
    }

    public void UpdateEnergy(float current, float max)
    {
        energyBar.value = current / max;
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        if (isPaused)
            Time.timeScale = 0f; // pause game
        else
            Time.timeScale = 1f; // resume game
    }

    public void CloseStartMenu()
    {
        if (startMenu != null) startMenu.SetActive(false);
    }
}
