using UnityEngine;
using NaughtyAttributes;

public partial class PlayerController : Singelton<PlayerController>
{
    [HorizontalLine(color: EColor.Green)]
    [Header("Movement Parameters")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _movementSpeed;
    private Vector3 _movementDirection;

    [HorizontalLine(color: EColor.Blue)]
    [Header("Rotation Parameters")]
    [SerializeField] private Transform _rotationTarget;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _gravityMultiplier;
    private RotationHandler rotationHandler;
    private MovemementHandler movemementHandler;

    [Header("Consuming Parameters")]
    [SerializeField] private Transform _consumingPoint;
    public Transform ConsumingPoint => _consumingPoint;
    public bool IsConsuming;


    [SerializeField] private GravityHandler gravityHandler;

    void Start()
    {
        movemementHandler = new MovemementHandler(_characterController, Camera.main.transform);
        rotationHandler = new RotationHandler(_rotationTarget);
        gravityHandler = new GravityHandler(_characterController, _gravityMultiplier);
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    private void FixedUpdate()
    {
        gravityHandler.HandleGravity();
    }

    void HandleRotation()
    {
        _movementDirection = GetFixedMovementDirection();
        if (_movementDirection.magnitude == 0)
        {
            return;
        }
        Vector3 rotationPosition = _movementDirection + transform.position;
        rotationHandler.Rotate(rotationPosition, _rotationSpeed);
    }

    void HandleMovement()
    {
        if (_movementDirection.magnitude == 0)
        {
            return;
        }
        movemementHandler.Move(_movementDirection, _movementSpeed);
    }

    Vector3 GetFixedMovementDirection()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        return inputVector;
    }


}
