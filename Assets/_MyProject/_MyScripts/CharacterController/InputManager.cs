using UnityEngine;

public class InputManager
{
    public Vector3 GetMovementInput()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
}


