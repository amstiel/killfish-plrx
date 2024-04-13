using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField]private PlayerController playerController;
    private EnemyController enemyController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out enemyController);
        StartBattle(enemyController, playerController);
    }

    private void StartBattle(EnemyController enemyController, PlayerController playerController) 
    {
        WorldInfo.Instance().SetState(WorldInfo.GameState.Fight);
        enemyController.deadEvent.AddListener(EndBattle);
        playerController.deadEvent.AddListener(EndBattle);
        playerController.SetTargetController(enemyController);
        enemyController.SetTargetController(playerController);
    }

    private void EndBattle() 
    {
        if (enemyController != null)
        {
            enemyController.EndBattle();
        }
        playerController.EndBattle();
        WorldInfo.Instance().SetState(WorldInfo.GameState.Moving);
    }

}
