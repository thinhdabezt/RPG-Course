using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(StateMachine stateMachine, string stateName, Player player) : base(stateMachine, stateName, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, player.rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();
        
        if (player.moveInput.x == player.facingDir && player.wallDetected)
        {
            return;
        }

        if(player.moveInput != Vector2.zero)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}
