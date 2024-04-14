using UnityEngine;
using UnityEngine.Events;

public class BattleController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject speachRenderer;
    private EnemyController enemyController;
    private UnityEvent eventSpeechEnd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out enemyController);
        StartDialogue();
    }

    private void StartDialogue()
    {
        WorldInfo.Instance().SetState(WorldInfo.GameState.Speaking);
        eventSpeechEnd = enemyController.StartDialogue(speachRenderer);
        eventSpeechEnd.AddListener(() => StartBattle(enemyController, playerController));
        speachRenderer.SetActive(true);
    }
    private void EndDialogue()
    {
        WorldInfo.Instance().SetState(WorldInfo.GameState.Moving);
        eventSpeechEnd.RemoveAllListeners();
        speachRenderer.SetActive(false);
    }

    private void StartBattle(EnemyController enemyController, PlayerController playerController)
    {
        speachRenderer.SetActive(false);
        eventSpeechEnd.RemoveAllListeners();
        WorldInfo.Instance().SetState(WorldInfo.GameState.Fight);
        enemyController.deadEvent.AddListener(EndBattle);
        playerController.deadEvent.AddListener(EndBattle);
        playerController.SetTargetController(enemyController);
        enemyController.SetTargetController(playerController);
    }

    private void EndBattle()
    {
        speachRenderer.SetActive(true);
        enemyController.StartAfterDeathDialogue(speachRenderer);
        eventSpeechEnd.AddListener(EndDialogue);

        if (enemyController != null)
        {
            enemyController.EndBattle();
        }
        playerController.EndBattle();
        WorldInfo.Instance().SetState(WorldInfo.GameState.Speaking);
    }
}
