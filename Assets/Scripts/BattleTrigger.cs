using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField]private PlayerController playerController;
    private EnemyController enemyController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out enemyController);
        playerController.SetTargetController(enemyController);
        enemyController.SetTargetController(playerController);

        StartBattle();
    }

    private void StartBattle() 
    {
        enemyController.deadEvent.AddListener(EndBattle);
        playerController.deadEvent.AddListener(EndBattle);
        WorldInfo.Instance().SetState(WorldInfo.GameState.Fight);
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
