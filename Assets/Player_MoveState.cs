using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(StateMachine stateMachine, string stateName, Player player) : base(stateMachine, stateName, player)
    {
    }

    public override void Update()
    {
        base.Update();

        if(player.moveInput == Vector2.zero && player.wallDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }

        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y);
    }
}
