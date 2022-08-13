using UnityEngine;
using UnityEngine.InputSystem;

public class MenuMenager : MonoBehaviour
{
    private UIControls playerInputAction;
    void Awake()
    {
        


        playerInputAction = new UIControls();
        playerInputAction.Move.Enable();

        playerInputAction.Move.Approval.performed += Save;

    }
    void Save(InputAction.CallbackContext ctx)
    {
        SaveMenager.instance.Load();

    }
}
