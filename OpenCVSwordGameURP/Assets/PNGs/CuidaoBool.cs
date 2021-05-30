using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuidaoBool : MonoBehaviour
{
    public bool CuidaoStarted;
    public AudioSource source;

    public void StartCuidao()
    {
        CuidaoStarted = true;
        source.Play();
    }

    public void EndCuidao()
    {
        CuidaoStarted = false;
    }
}
