using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float _limitX = 2;
    public float _xSpeed = 25;
    public float _forwardSpeed = 2;

    // Update is called once per frame
    void Update()
    {
        
        float _touchXDelta = 0;
        float _newX = 0;
        if (Input.GetMouseButton(0))
        {
            _touchXDelta = Input.GetAxis("Mouse X");
        }
        _newX = transform.position.x + _xSpeed * _touchXDelta * Time.deltaTime;
        _newX = Mathf.Clamp(_newX, -_limitX, _limitX);


        Vector3 newPosition = new Vector3(_newX, transform.position.y, transform.position.z + _forwardSpeed * Time.deltaTime);
        transform.position = newPosition;
        
    }
}
