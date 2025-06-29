using UnityEngine;

public class Player_DashState : EntityState
{
    private float originalGravityScale;
    private int dashDir;

    public Player_DashState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CancelDashIfNeed();

        stateTimer = player.dashDuration;

        if (player.moveInput.x != 0)
        {
            dashDir = ((int)player.moveInput.x);
        }
        else
        {
            dashDir = player.facingDir;
        }

        originalGravityScale = player.rb.gravityScale;
        player.rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.dashSpeed * dashDir, 0);

        if (stateTimer <= 0)
        {
            if(player.groundDetected)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.fallState);
            }
        }
    }

    override public void Exit()
    {
        base.Exit();

        player.SetVelocity(0, 0);
        player.rb.gravityScale = originalGravityScale;
    }

    private void CancelDashIfNeed()
    {
        if (player.wallDetected)
        {
            if (player.groundDetected)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.fallState);
            }
        }
    }
}
