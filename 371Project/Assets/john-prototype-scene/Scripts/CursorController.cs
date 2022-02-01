using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private GameInput _input;
    private Camera mainCamera;
    [HideInInspector] public bool _towerEquipped;

    public GameObject tower;
    private void Awake()
    {
        _input = new GameInput();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Start()
    {
        _input.Mouse.Click.started += _ => StartedClick();
        _input.Mouse.Click.performed += _ => EndedClick();
    }

    public void EquipTowerToggle()
    {
        _towerEquipped = !_towerEquipped;
    }

    private void DetectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(_input.Mouse.Position.ReadValue<Vector2>());
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, 1000.0f);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider != null)
                {
                    Debug.Log("3D Hit" + hit.collider.tag);
                    IClickable clickable = hit.collider.GetComponent<IClickable>();
                    if (clickable != null)
                    {
                        clickable.OnClicked();
                    }
                }
            }
    }

    private void StartedClick()
    {
        
    }

    private void TryToPlaceTower()
    {
        Ray ray = mainCamera.ScreenPointToRay(_input.Mouse.Position.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.CompareTag("PlaceableSurf"))
            {
                // get the normal of the surface at that point
                Vector3 spawnPos = hit.point;
                Instantiate(tower, spawnPos, Quaternion.LookRotation(hit.normal));
                _towerEquipped = false;
            }
        }
    }

    private void EndedClick()
    {
        if (_towerEquipped)
        {
            TryToPlaceTower();
        }
        else
        {
            DetectObject();
        }
    }
}
