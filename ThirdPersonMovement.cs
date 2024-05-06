using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Assigned Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;

    [Header("Player Movement Values")]
    public float maximumSpeed;
    public float rotationSpeed;
    public float jumpForce;
    public float jumpButtonGracePeriod;
    private float _yForce;
    private float originalStepOffset;
    private float? _lastGroundedTime;
    private float? _jumpButtonPressedTime;

    // Start is called before the first frame update
    void Start()
    {
        originalStepOffset = _characterController.stepOffset;    
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude /= 2;
        }
        _animator.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);

        float speed = inputMagnitude * maximumSpeed;
        movementDirection.Normalize();

        _yForce += Physics.gravity.y * Time.deltaTime;

        if(_characterController.isGrounded)
        {
            _lastGroundedTime = Time.time;
        }

        if(Input.GetButtonDown("Jump"))
        {
            _jumpButtonPressedTime = Time.time;
        }

        if (Time.time - _lastGroundedTime <= jumpButtonGracePeriod)
        {
            _characterController.stepOffset = originalStepOffset;
            _yForce = -0.5f;

            if (Time.time - _jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                _yForce = jumpForce;
                _jumpButtonPressedTime = null;
                _lastGroundedTime = null;
            }
        }
        else
        {
            _characterController.stepOffset = 0;
        }

        Vector3 velocity = movementDirection * speed;
        velocity.y = _yForce;

        _characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    } //end Update

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Door"))
        {
            hit.gameObject.GetComponent<Doors>().OpenTheDoor();
        }
    }
}
