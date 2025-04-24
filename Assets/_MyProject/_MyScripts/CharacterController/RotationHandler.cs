using UnityEngine;

public class RotationHandler
{
    private Transform _rotationTarget;

    public RotationHandler(Transform rotationTarget)
    {
        _rotationTarget = rotationTarget;
    }

    public void Rotate(Vector3 targetPosition, float rotationSpeed)
    {
        Vector3 direction = (targetPosition - _rotationTarget.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        _rotationTarget.rotation = Quaternion.Slerp(_rotationTarget.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
