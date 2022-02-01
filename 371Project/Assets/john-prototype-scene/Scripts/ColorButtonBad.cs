using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonBad : MonoBehaviour
{
    public CursorController cursor;
    private Image _button;

    public Color towerSelected = Color.red;

    private void Awake()
    {
        _button = GetComponent<Image>();
    }

    void Update()
    {
        if (cursor._towerEquipped)
        {
            _button.color = towerSelected;
        }
        else
        {
            _button.color = Color.white;
        }
    }
}
