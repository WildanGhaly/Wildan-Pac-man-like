using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidBody;

    private Coroutine _powerupCoroutine;

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Transform _camera;

    [SerializeField]
    private float _powerupDuration;

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

    public void PickPowerUp()
    {
        if (_powerupCoroutine != null)
        {
            StopCoroutine(_powerupCoroutine);
        }

        _powerupCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        if (OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }

        _speed *= 2;

        yield return new WaitForSeconds(_powerupDuration);

        _speed /= 2;

        if (OnPowerUpStop != null)
        {
            OnPowerUpStop();
        }

        _powerupCoroutine = null;
    }
}
