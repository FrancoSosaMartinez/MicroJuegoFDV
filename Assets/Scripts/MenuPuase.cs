using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public GameObject menuPauseUI;
    private bool isPaused = false;

    public void Pause()
    {
        menuPauseUI.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;
    }

    public void Resume()
    {
        menuPauseUI.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;
    }
}
