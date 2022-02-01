using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBasePoint : MonoBehaviour
{
    public GameObject LoseText;
    private void OnTriggerEnter(Collider other)
    {
        // must be enemy
        Time.timeScale = 0;
        LoseText.SetActive(true);
    }
}
