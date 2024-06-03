using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 movementInput { get; private set; }
    public bool jumpInput { get; private set; }
    public bool attackInput { get; private set; }
    public bool blockDownInput { get; private set; }
    public bool blockUpInput { get; private set; }
    public bool rollInput { get; private set; }
    void Update()
    {
        if (!Pause.bGamePaused)
        {
            movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            jumpInput = Input.GetKeyDown(KeyCode.Space);
            attackInput = Input.GetMouseButtonDown(0);
            blockDownInput = Input.GetMouseButtonDown(1);
            blockUpInput = Input.GetMouseButtonUp(1);
            rollInput = Input.GetKeyDown(KeyCode.LeftControl);
        }
    }
}
