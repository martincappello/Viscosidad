﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private const string SELECTABLE_TAG = "Selectable";

        [SerializeField] private Material highlightMaterial;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] public Material selectableMaterial;
        private GameObject _selectedObject;
        private Transform _selection;
        private bool _selectionLocked;
        private bool _displayInfoMode;
        public Camera camera;
        public GameObject aimingDot;
        public GameObject[] selectableElements;

        private void Update()
        {
            if (_displayInfoMode)
            {
                if (!_selectionLocked)
                {
                    var cam = camera.transform;
                    var ray = new Ray(cam.position, cam.forward);
                    RaycastHit hit;

                    if (!Physics.Raycast(ray, out hit))
                    {
                        if (_selection != null)
                        {
                            DeselectObject();
                        }
                    }
                    else
                    {
                        var selected = FindSelectedObject(hit);
                        _selection = selected;
                    }
                }
            }
        }

        private void DeselectObject()
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = selectableMaterial;
            _selection = null;
            _selectedObject.GetComponent<DisplayMenu>().HideMenu();
        }

        private Transform FindSelectedObject(RaycastHit hit)
        {
            var selection = hit.transform;
            if (selection.CompareTag(SELECTABLE_TAG))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                    _selectedObject = hit.collider.gameObject;
                    _selectedObject.GetComponent<DisplayMenu>().ShowMenu();
                }
    
                return selection;
            }
            return null;
        }
        public void InfoDisplayModeToggle()
        {
            if (!_displayInfoMode)
            {
                aimingDot.SetActive(true);
                SetMaterials();
                _displayInfoMode = true;
            }
            else
            {
                SetMaterials();
                _displayInfoMode = false;
                _selectionLocked = false;
                aimingDot.SetActive(false);
            }
            
        }

        public void SelectionLockToggle()
        {
            if (_selectionLocked)
            {
                _selectionLocked = false;
                aimingDot.SetActive(true);
            }
            else
            {
                if (_selection != null)
                {
                    _selectionLocked = true;
                    aimingDot.SetActive(false);
                }
            }
        }

        private void SetMaterials()
        {
            if (!_displayInfoMode)
            {
                foreach (GameObject element in selectableElements)
                {
                    element.GetComponent<Renderer>().material = selectableMaterial;
                }
            }
            else
            {
                foreach (GameObject element in selectableElements)
                {
                    element.GetComponent<Renderer>().material = defaultMaterial;
                }
            }
        }
    }
}