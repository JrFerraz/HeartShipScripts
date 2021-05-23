using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfazGame : MonoBehaviour
{
    public Canvas pauseMenu;
    public static bool isPaused;
    public float tiempoJuego;

    private void Awake()
    {
        isPaused = false;
    }
    private void Update()
    {
        pausarJuego();
    }



    public void volverAlMenuPorPausa()
    {
        Time.timeScale = tiempoJuego;
        SceneManager.LoadScene(0);
    }

    public void volverAlMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void regresarJuego()
    {
        pauseMenu.gameObject.SetActive(false);
        isPaused = false;
        if (tiempoJuego == 0.7f) FindObjectOfType<audioManager>().Play("TicTac");
        Time.timeScale = tiempoJuego;
    }

    public void salirJuego()
    {
        Application.Quit();
    }

    public void pausarJuego()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pauseMenu.gameObject.SetActive(true);
            isPaused = true;
            tiempoJuego = Time.timeScale;
            Time.timeScale = 0f;
            if(tiempoJuego == 0.7f) FindObjectOfType<audioManager>().Pause("TicTac");
            //FindObjectOfType<audioManager>().allPaused();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            pauseMenu.gameObject.SetActive(false);
            isPaused = false;
            if (tiempoJuego == 0.7f) FindObjectOfType<audioManager>().Play("TicTac");
            Time.timeScale = tiempoJuego;
        }
    }
}
