using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlAudio : MonoBehaviour
{
    void Start()
    {
        silenciarAudio();
    }

    public void silenciarAudio()
    {
        if (buttonActions.silenciarTodo) FindObjectOfType<audioManager>().allMuted();
    }
}
