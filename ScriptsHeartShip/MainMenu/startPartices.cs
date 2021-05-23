using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class startPartices : MonoBehaviour
{
    public ParticleSystem pStarsIntro;
    public TextMeshProUGUI startText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI webText;
    public TextMeshProUGUI exitText;
    Animator animTitleText;
    public Button startButton;
    public Button highScoresButton;
    public Button AudioButton;
    public Button webButton;
    public Button exitButton;

    public TextMeshProUGUI audioText;

    private void Start()
    {
        FindObjectOfType<audioManager>().Play("HeartBeat");
        Cursor.visible = true;
        if (buttonActions.silenciarTodo)
        {
            FindObjectOfType<audioManager>().allMuted();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!pStarsIntro.isEmitting)
            {
                pStarsIntro.Play();
                startText.enabled = false;
                FindObjectOfType<audioManager>().Play("SpaceKey");
                FindObjectOfType<audioManager>().Stop("HeartBeat");
                FindObjectOfType<audioManager>().Play("Intro");
                Invoke("mostrarMainMenu",3f);

            }
        }
        if (pStarsIntro.isEmitting && pStarsIntro.emissionRate <=500)
        {
            if (Time.time <= 5)
            {
                pStarsIntro.emissionRate += 1f * Time.deltaTime;
            }
            else
            {
                pStarsIntro.emissionRate += 20f * Time.deltaTime;
            }
        }
    }

    void mostrarMainMenu()
    {
        titleText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
        highScoresButton.gameObject.SetActive(true);
        AudioButton.gameObject.SetActive(true);
        Invoke("sacarBtnAux", 1f);
        animTitleText = titleText.gameObject.GetComponent<Animator>();
        animTitleText.SetBool("isPalpiting", true);
        if (buttonActions.silenciarTodo)
        {
            audioText.text = "Audio: Off";
        }
    }
    void sacarBtnAux()
    {
        webButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
    }
}
