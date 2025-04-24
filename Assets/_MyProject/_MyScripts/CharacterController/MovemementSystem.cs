using UnityEngine;
public class MovemementHandler
{
    private CharacterController _characterController;
    private Transform _cameraTransform;

    public MovemementHandler(CharacterController characterController, Transform cameraTransform)
    {
        this._characterController = characterController;
        this._cameraTransform = cameraTransform;
    }
    public void SetupCamera(Transform cameraTransform)
    {
        this._cameraTransform = cameraTransform;
    }
    public void Move(Vector3 direction, float speed)
    {
        Vector3 moveDirection = _cameraTransform.forward * direction.z + _cameraTransform.right * direction.x;
        // Ensure movement stays on the horizontal plane by zeroing out the y component
        moveDirection.y = 0;
        moveDirection.Normalize();
        _characterController.Move(moveDirection * speed * Time.deltaTime);
    }
}


