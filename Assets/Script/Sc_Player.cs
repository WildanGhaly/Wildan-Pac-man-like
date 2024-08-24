using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidBody;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Transform _camera;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDirection = _camera.right;
        Vector3 verticalDirection = _camera.forward;

        horizontalDirection.y = 0;
        verticalDirection.y = 0;

        horizontalDirection.Normalize();
        verticalDirection.Normalize();

        Vector3 movementDirection = (horizontalDirection * horizontal) + (verticalDirection * vertical);

        _rigidBody.velocity = movementDirection * _speed * Time.fixedDeltaTime;
    }
}
