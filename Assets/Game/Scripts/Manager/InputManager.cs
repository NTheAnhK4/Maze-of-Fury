
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public Vector3 GetDirection()
    {
        return Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.up;
    }
}
