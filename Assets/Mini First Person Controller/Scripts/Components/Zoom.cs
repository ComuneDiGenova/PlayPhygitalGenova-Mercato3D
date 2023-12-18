﻿using UnityEngine;

namespace Mini_First_Person_Controller.Scripts.Components
{
    [ExecuteInEditMode]
    public class Zoom : MonoBehaviour
    {
        private Camera _camera;
        public float defaultFOV = 60;
        public float maxZoomFOV = 15;
        [Range(0, 1)]
        public float currentZoom;
        public float sensitivity = 1;


        private void Awake()
        {
            // Get the camera on this gameObject and the defaultZoom.
            _camera = GetComponent<Camera>();
            if (_camera)
            {
                defaultFOV = _camera.fieldOfView;
            }
        }

        private void Update()
        {
            // Update the currentZoom and the camera's fieldOfView.
            currentZoom += Input.mouseScrollDelta.y * sensitivity * .05f;
            currentZoom = Mathf.Clamp01(currentZoom);
            _camera.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
        }
    }
}
