using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Min(0.01f)] public float speed = 10f;
    [Min(0.01f)] public float turnSmoothness = .1f;

    private float turnSmoothVelocity;

    private CharacterController _charController;
    private PlayerController _inputHandler;
    private PlayerAnimationHandler _animationHandler;

    void Awake()
    {
        _inputHandler = new PlayerController();
        _charController = GetComponent<CharacterController>();
        _animationHandler = GetComponent<PlayerAnimationHandler>();
    }

    void OnEnable() { _inputHandler?.Enable(); }

    void OnDisable() { _inputHandler?.Disable(); }

    void Update()
    {
        Vector2 input = _inputHandler.Standard.Movement.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

        if (direction.magnitude > .1f)
        {
            _charController.Move(direction * speed * Time.deltaTime);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothness);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            _animationHandler.PlayRunAnimation();
        }
        else
        {
            _animationHandler.PlayIdleAnimation();
        }
    }
}
