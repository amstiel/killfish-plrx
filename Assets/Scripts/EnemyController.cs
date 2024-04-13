using System.Collections;
using UnityEngine;

public class EnemyController : Controller
{
   
    [SerializeField] private float cooldown;
    [SerializeField] private int coins;

    public int Coins => coins;

    public override void SetTargetController(Controller target) 
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

    private IEnumerator StartTimerAttack() 
    {
        while (WorldInfo.Instance().gameState == WorldInfo.GameState.Fight) 
        {
            Attack();
            yield return new WaitForSeconds(cooldown);
        } 
    }
}
