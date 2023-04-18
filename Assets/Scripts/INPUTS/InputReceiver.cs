using UnityEngine;
using UnityEngine.InputSystem;


public class InputReceiver : MainBehaviour
{
    private Player playerRef;
    [SerializeField] private Camera m_MainCamera;
    protected override void OnAwake()
    {
        playerRef = GetComponent<Player>();
        m_MainCamera = Camera.main;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!playerRef.IsGameplaymode) return;
        playerRef.InputDir = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!playerRef.IsGameplaymode) return;
        
        if (context.performed)
        {
            playerRef.ListOfActions.AddAction(new PlayerAction(PlayerActionsType.SHOOT));
        }
        if (context.canceled)
        {
            playerRef.ListOfActions.ConsumeAllActions((PlayerActionsType.SHOOT));

        }
    }
    public Vector2 MousePosition()
    {
        if (!playerRef.IsGameplaymode) return Vector2.zero;
        return m_MainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }


    public void OnGamePause(InputAction.CallbackContext context)
    {
        if (!playerRef.IsGameplaymode) return;
        if (context.performed)
        {
            playerRef.ListOfActions.AddAction(new PlayerAction(PlayerActionsType.PAUSE));
        }
    }
}
