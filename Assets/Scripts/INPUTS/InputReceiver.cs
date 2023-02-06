using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputReceiver : MainBehaviour
{
    private Player playerRef;
    protected override void OnAwake()
    {
        playerRef = Player.Instance;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        playerRef.Direction = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerRef.ListOfActions.AddAction(new PlayerAction(PlayerActionsType.SHOOT));
        }
    }
    public Vector2 MousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    //public void OnRun(InputAction.CallbackContext context)
    //{
    //    action = new PlayerAction(PlayerActionsType.RUN);
    //    playerRef.DesiredActions.AddAction(action);
    //    //if (context.performed)
    //    //    isRunning = true;
    //    //else
    //    //    isRunning = false;
    //    playerRef.Islide = context.performed;
    //}




    //public void OnGamePause(InputAction.CallbackContext context)
    //{
    //    //if (context.performed && !manager.IsOver)
    //    //  panel.PauseMenu();
    //}
}