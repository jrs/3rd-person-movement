using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Assigned Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _cameraTransform;

    [Header("Player Movement Values")]
    public float maximumSpeed;
    public float rotationSpeed;
    [Header("Jumping Values")]
    public float jumpForce;
    public float jumpButtonGracePeriod;
    private float _yForce;
    private float originalStepOffset;
    private float? _lastGroundedTime;
    private float? _jumpButtonPressedTime;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isJumping;

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
        movementDirection = Quaternion.AngleAxis(_cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
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
            _animator.SetBool("IsGrounded", true);
            _isGrounded = true;
            _animator.SetBool("IsJumping", false);
            _isJumping = false;
            //_animator.SetBool("IsFalling", false);

            if (Time.time - _jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                _yForce = jumpForce;
                _animator.SetBool("IsJumping", true);
                _isJumping = true;
                _jumpButtonPressedTime = null;
                _lastGroundedTime = null;
            }
        }
        else
        {
            _characterController.stepOffset = 0;
            _animator.SetBool("IsGrounded", false);
            _isGrounded = false;
        }

        Vector3 velocity = movementDirection * speed;
        velocity.y = _yForce;

        _characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            _animator.SetBool("IsMoving", true);

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }
    } //end Update

    void OnApplicationFocus(bool hasFocus)
    {
        if(hasFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
