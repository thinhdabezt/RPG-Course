using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private float attackVelocityTimer;
    private float lastTimeAttack;

    private const int FirstComboIndex = 1;
    private int comboIndex = 1;
    private int comboLimit = 3;
    private int attackDir;

    private bool comboAttackQueued;

    public Player_BasicAttackState(StateMachine stateMachine, string animBoolName, Player player) : base(stateMachine, animBoolName, player)
    {
        if(comboLimit != player.attackVelocity.Length)
        {
            Debug.LogWarning("Combo limit does not match the length of attack velocity array!");
            comboLimit = player.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();

        comboAttackQueued = false;

        ResetComboIndex();

        if (player.moveInput.x != 0)
        {
            attackDir = ((int)player.moveInput.x);
        }
        else
        {
            attackDir = player.facingDir;
        }

        anim.SetInteger("basicAttackIndex", comboIndex);

        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();

        HandleAttackVelocity();

        if(input.Player.Attack.WasPressedThisFrame())
        {
            QueuedNextAttack();
        }

        if (triggerCalled)
        {
            HandleStateExit();

        }
    }

    public override void Exit()
    {
        base.Exit();

        comboIndex++;

        lastTimeAttack = Time.time;
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    private void QueuedNextAttack()
    {
        if (comboIndex < comboLimit)
        {
            comboAttackQueued = true;
        }
    }

    public void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if(attackVelocityTimer < 0)
        {
            player.SetVelocity(0, rb.linearVelocity.y);
        }
    }

    private void ResetComboIndex()
    {
        if (Time.time > lastTimeAttack + player.comboResetTime)
        {
            comboIndex = FirstComboIndex;
        }

        if (comboIndex > comboLimit)
        {
            comboIndex = FirstComboIndex;
        }
    }

    private void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;

        player.SetVelocity(player.attackVelocity[comboIndex-1].x * attackDir, player.attackVelocity[comboIndex-1].y);
    }
}
