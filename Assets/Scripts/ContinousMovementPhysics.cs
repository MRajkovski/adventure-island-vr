using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContinousMovementPhysics : MonoBehaviour
{
    public float speed = 15f;
    public float turnSpeed = 60;
    public float jumpHeight = 10f;
    public InputActionProperty moveInputSource;
    public InputActionProperty turnInputSource;
    public InputActionProperty jumpInputSource;  
    public Rigidbody playerRigidbody;
    public LayerMask groundLayer;
    public Transform directionSource;
    public CapsuleCollider bodyCollider;
    public Transform turnSource;
    public bool onlyMoveWhenGrounded = false;

    private float jumpVelocity = 7;
    private Vector2 inputMoveAxis;
    private float inputTurnAxis;
    private bool isGrounded;


    void Update()
    {
        inputMoveAxis = moveInputSource.action.ReadValue<Vector2>();
        inputTurnAxis = turnInputSource.action.ReadValue<Vector2>().x;

        bool jumpInput = jumpInputSource.action.WasPerformedThisFrame();

        if (jumpInput && isGrounded)
        {
            jumpVelocity = Mathf.Sqrt(2 * -Physics.gravity.y * jumpHeight);
            playerRigidbody.velocity = Vector3.up * jumpVelocity;
        }
    }
    private void FixedUpdate()
    {
        isGrounded = CheckIfGrounded();
        if (!onlyMoveWhenGrounded || (onlyMoveWhenGrounded && isGrounded))
        {
            Quaternion yaw = Quaternion.Euler(0, directionSource.eulerAngles.y, 0);
            Vector3 direction = yaw * new Vector3(inputMoveAxis.x, 0, inputMoveAxis.y);

            Vector3 targetMovePosition = playerRigidbody.position + direction * Time.fixedDeltaTime * speed;

            Vector3 axis = Vector3.up;
            float angle = turnSpeed * Time.fixedDeltaTime * inputTurnAxis;

            Quaternion q = Quaternion.AngleAxis(angle, axis);

            playerRigidbody.MoveRotation(playerRigidbody.rotation * q);

            Vector3 newPosition = q * (targetMovePosition - turnSource.position) + turnSource.position;

            playerRigidbody.MovePosition(newPosition);
        }   
    }
    public bool CheckIfGrounded()
    {
        Vector3 start = bodyCollider.transform.TransformPoint(bodyCollider.center);
        float rayLength = bodyCollider.height / 2 - bodyCollider.radius + 0.45f;
        bool hasHit = Physics.SphereCast(start, bodyCollider.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
}
