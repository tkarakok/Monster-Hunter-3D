using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CameraController : Singleton<CameraController>
{
    public float cameraChangeTime;
    [SerializeField] private Transform _target;
    [SerializeField] private Camera _battleCamera;
    private Vector3 _offset;
    private Vector3 _mainCameraPosition;
    private Quaternion _mainCameraQuaternion;


    private void Start()
    {
        _offset = gameObject.transform.position - _target.position;
        
    }

    private void LateUpdate()
    {
        if (StateManager.Instance.GameState == GameState.InGame)
        {
            Vector3 targetPosition = _target.position + _offset;
            transform.position = targetPosition;
        }
        
        

    }

    public void ChangeCamera()
    {
        _mainCameraPosition = transform.position;
        _mainCameraQuaternion = transform.rotation;
        transform.DOMove(_battleCamera.transform.position, cameraChangeTime);
        transform.DORotateQuaternion(_battleCamera.transform.rotation, cameraChangeTime);
    }

    public void BackToMainCamera()
    {
        transform.DOMove(_mainCameraPosition, cameraChangeTime);
        transform.DORotateQuaternion(_mainCameraQuaternion, cameraChangeTime);
    }

    
}
