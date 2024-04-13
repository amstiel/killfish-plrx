using System.Collections;
using UnityEngine;

public class EnemyController : CharactersController
{
   
    [SerializeField] private float cooldown;

    public override void SetTargetController(CharactersController target) 
    {
        base.SetTargetController(target);
        StartCoroutine(StartTimerAttack());
    }

    public override void EndBattle()
    {
        base.EndBattle();
        StopAllCoroutines();
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void Dead() 
    {
        PlayerController playerController = (PlayerController)targetController;
        playerController.AddCoins(coins);
        base.Dead();

    }

    private IEnumerator StartTimerAttack() 
    {
        while (WorldInfo.Instance().gameState == WorldInfo.GameState.Fight) 
        {
            Attack();
            yield return new WaitForSeconds(cooldown);
        } 
    }
}
