using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public static bool isPaused;
    
    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
;    }

    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {

            if (isPaused)
            {
                ExitPause();
            }

            else
            {
                StartPause();
            }            
        }
    }

    private void StartPause() { 
    
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ExitPause() {

        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void SalirJuego()
    {
        Application.Quit();
    }

    public void MenuPrincipal()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1f;
    }

    public void Reiniciar() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
