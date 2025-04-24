using UnityEngine;

public class GravityHandler
{
    private CharacterController _characterController;
    private float _gravityMultiplier;

    public GravityHandler(CharacterController characterController, float gravityMultiplier)
    {
        _characterController = characterController;
        _gravityMultiplier = gravityMultiplier;
    }

    public void HandleGravity()
    {
        _characterController.Move(Physics.gravity * _gravityMultiplier * Time.deltaTime);
    }

}
