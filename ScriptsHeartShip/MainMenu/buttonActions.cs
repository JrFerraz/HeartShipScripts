using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonActions : MonoBehaviour
{
    public Image transicion;
    public static bool silenciarTodo = false;
    public TextMeshProUGUI audioText;
    public void cargarJuego()
    {
        FindObjectOfType<audioManager>().Play("SpaceKey");
        StartCoroutine("TransicionJuego");
    }
    
    public void irHighScores()
    {
        FindObjectOfType<audioManager>().Play("SpaceKey");
        DontDestroyOnLoad(FindObjectOfType<audioManager>());
        StartCoroutine("TransicionHighScores");
    }

    public void verWeb()
    {
        Application.OpenURL("file:///C:/Users/jfero/Desktop/Plantillas%20Web/WebHeartShip/index.html");
    }

    public void cerrarJuego()
    {
        Application.Quit();
    }
    public void silencio()
    {
        if (!silenciarTodo)
        {
            FindObjectOfType<audioManager>().allMuted();
            silenciarTodo = true;
            audioText.text = "Audio: Off";
        }
        else
        {
            FindObjectOfType<audioManager>().allSounding();
            silenciarTodo = false;
            audioText.text = "Audio: On";
        }
    }

    IEnumerator TransicionJuego()
    {
        transicion.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game");
    }
    IEnumerator TransicionHighScores()
    {
        transicion.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("HighScores");
    }
}
