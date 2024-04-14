using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : CharactersController
{
   
    [SerializeField] private float cooldown;
    [SerializeField] private DialogTextRenderer textRenderer;
    public override void SetTargetController(CharactersController target) 
    {
        base.SetTargetController(target);
        StartCoroutine(StartTimerAttack());
    }

    public UnityEvent StartDialogue(GameObject speachRenderer) 
    {
        textRenderer.textRendererObject = speachRenderer;
        textRenderer.enabled = true;
        return textRenderer.spechEndEvent;

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
