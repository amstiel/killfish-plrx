using UnityEngine;
using UnityEngine.Events;

public class BattleController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject speachRenderer;
    private EnemyController enemyController;
    private FriendController friendController;
    private UnityEvent eventSpeechEnd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Enemy")
        {
            collision.TryGetComponent(out enemyController);
            StartEnemyDialogue();
        }

        if (collision.tag == "Friend")
        {
            collision.TryGetComponent(out friendController);
            StartFriendsDialogue();
        }
    }

    private void StartEnemyDialogue()
    {
        WorldInfo.Instance().SetState(WorldInfo.GameState.Speaking);
        eventSpeechEnd = enemyController.StartDialogue(speachRenderer);
        eventSpeechEnd.AddListener(() => StartBattle(enemyController, playerController));
        speachRenderer.SetActive(true);
    }

    private void StartFriendsDialogue()
    {
        WorldInfo.Instance().SetState(WorldInfo.GameState.Speaking);
        eventSpeechEnd = enemyController.StartDialogue(speachRenderer);
        speachRenderer.SetActive(true);
    }
    private void EndDialogue()
    {
        WorldInfo.Instance().SetState(WorldInfo.GameState.Moving);
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
        eventSpeechEnd.AddListener(Destroyer);

        if (enemyController != null)
        {
            enemyController.EndBattle();
        }
        playerController.EndBattle();
        WorldInfo.Instance().SetState(WorldInfo.GameState.Speaking);
    }

    public void Destroyer()
    {
        if (enemyController != null && playerController.Hp > 0)
        {
            enemyController.Destroy();
            eventSpeechEnd.RemoveAllListeners();
        }

    }
}
