using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

namespace MagicLeap
{
    public class ControlScript : MonoBehaviour
    {
        public GameObject _player;
        private MLInputController _controller;

        public MeshRenderer meshRenderer;
        public MeshingVisualizer _meshingVisualizer;

        private MeshingVisualizer.RenderMode _renderMode = MeshingVisualizer.RenderMode.Wireframe;
        private int _renderModeCount;

        /*
        private const float _rotationSpeed = 30.0f;
        private const float _moveSpeed = 1.2f;
        private bool _enabled = false;
        private bool _bumper = false;
        */
        private const float _distance = 1.0f;

        void Awake()
        {
            _player.SetActive(false);
            MLInput.Start();
            MLInput.OnControllerButtonDown += OnButtonDown;
            MLInput.OnControllerButtonUp += OnButtonUp;
            _controller = MLInput.GetController(MLInput.Hand.Left);

            _renderModeCount = System.Enum.GetNames(typeof(MeshingVisualizer.RenderMode)).Length;
        }

        void Start()
        {
            _meshingVisualizer.SetRenderers(_renderMode);
        }

        void OnDestroy()
        {
            MLInput.OnControllerButtonDown -= OnButtonDown;
            MLInput.OnControllerButtonUp -= OnButtonUp;
            MLInput.Stop();
        }

        /*
        void Update()
        {
            if (_bumper && _enabled)
            {
                _player.transform.Rotate(Vector3.up, +_rotationSpeed * Time.deltaTime);
            }
            CheckControl();
        }


        void CheckControl()
        {
            if (_controller.TriggerValue > 0.2f && _enabled)
            {
                _bumper = false;
                _player.transform.Rotate(Vector3.up, -_rotationSpeed * Time.deltaTime);
            }
            else if (_controller.Touch1PosAndForce.z > 0.0f && _enabled)
            {
                float X = _controller.Touch1PosAndForce.x;
                float Y = _controller.Touch1PosAndForce.y;
                Vector3 forward = Vector3.Normalize(Vector3.ProjectOnPlane(transform.forward, Vector3.up));
                Vector3 right = Vector3.Normalize(Vector3.ProjectOnPlane(transform.right, Vector3.up));
                Vector3 force = Vector3.Normalize((X * right) + (Y * forward));
                _player.transform.position += force * Time.deltaTime * _moveSpeed;
            }
        }



        void OnButtonDown(byte controller_id, MLInputControllerButton button)
        {
            if ((button == MLInputControllerButton.Bumper && _enabled))
            {
                _bumper = true;
            }
        }

        */



        void OnButtonUp(byte controller_id, MLInputControllerButton button)
        {
            if (button == MLInputControllerButton.HomeTap)
            {
                if(_player.activeSelf == false)
                {
                    Debug.Log("Home button pressed.");
                    _player.SetActive(true);
                    _player.transform.position = transform.position + transform.forward * _distance;
                    //_player.transform.rotation = transform.rotation;
                    //_enabled = true;

                } else
                {
                    _player.SetActive(false);
                    _player.transform.position = transform.position + transform.forward * _distance;
                    _player.SetActive(true);
                }
            }

            /*
            if (button == MLInputControllerButton.Bumper)
            {
                Debug.Log("Bumper pressed.");
                Debug.Log(meshRenderer.enabled);
                meshRenderer.enabled = !meshRenderer.enabled;
                Debug.Log(meshRenderer.enabled);
            }*/
        }

        void OnButtonDown(byte controller_id, MLInputControllerButton button)
        {
            if(button == MLInputControllerButton.Bumper)
            {
                _renderMode = (MeshingVisualizer.RenderMode)((int)(_renderMode + 1) % _renderModeCount);
                _meshingVisualizer.SetRenderers(_renderMode);

            }

        }
    }

}
