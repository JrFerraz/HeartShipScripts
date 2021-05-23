using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{

    public Image transicion;

    public TextMeshProUGUI TotalOro;
    public TextMeshProUGUI TotalPlata;
    public TextMeshProUGUI TotalBronce;

    public TextMeshProUGUI PuntosOro;
    public TextMeshProUGUI PuntosPlata;
    public TextMeshProUGUI PuntosBronce;

    public TextMeshProUGUI RachaOro;
    public TextMeshProUGUI RachaPlata;
    public TextMeshProUGUI RachaBronce;

    public TextMeshProUGUI DineroOro;
    public TextMeshProUGUI DineroPlata;
    public TextMeshProUGUI DineroBronce;

    public TextMeshProUGUI DineroGastadoOro;
    public TextMeshProUGUI DineroGastadoPlata;
    public TextMeshProUGUI DineroGastadoBronce;

    private void Start()
    {
        TotalOro.text = PlayerPrefs.GetInt("Oro",0) +"";
        TotalPlata.text = PlayerPrefs.GetInt("Plata",0) + "";
        TotalBronce.text = PlayerPrefs.GetInt("Bronce",0) + "";

        PuntosOro.text = PlayerPrefs.GetInt("puntosOro", 0) + "";
        PuntosPlata.text = PlayerPrefs.GetInt("puntosPlata", 0) + "";
        PuntosBronce.text = PlayerPrefs.GetInt("puntosBronce", 0) + "";

        RachaOro.text = PlayerPrefs.GetInt("rachaOro", 0) + "";
        RachaPlata.text = PlayerPrefs.GetInt("rachaPlata", 0) + "";
        RachaBronce.text = PlayerPrefs.GetInt("rachaBronce", 0) + "";

        DineroOro.text = PlayerPrefs.GetInt("dineroOro", 0) + "";
        DineroPlata.text = PlayerPrefs.GetInt("dineroPlata", 0) + "";
        DineroBronce.text = PlayerPrefs.GetInt("dineroBronce", 0) + "";
    }
    public void volverMainMenu()
    {
        SceneManager.MoveGameObjectToScene(FindObjectOfType<audioManager>().gameObject,SceneManager.GetActiveScene());
        FindObjectOfType<audioManager>().Play("SpaceKey");
        StartCoroutine("TransicionJuego");
    }

    IEnumerator TransicionJuego()
    {
        transicion.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        Destroy(FindObjectOfType<audioManager>().gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
