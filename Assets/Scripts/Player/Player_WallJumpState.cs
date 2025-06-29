using UnityEngine;

public class Player_WallJumpState : EntityState
{
    public Player_WallJumpState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        player.SetVelocity(player.wallJumpForce.x * -player.facingDir, player.wallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();
        
        if(rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }
        else if (player.wallDetected)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
