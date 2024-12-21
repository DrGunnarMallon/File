using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    private bool canFlipGravity = true;

    public bool CanFlipGravity()
    {
        return canFlipGravity;
    }

    public void EnableGravityFlip()
    {
        canFlipGravity = true;
    }

}
