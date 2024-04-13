using UnityEngine;

public class PlayerController : Controller
{
    [SerializeField] private float moveTime;
    [SerializeField] private EnemyController enemyTarget;
    private Animator animator; 


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartMove() 
    {
        animator.SetTrigger("startMove");
    }
    public void StopMove()
    {
        animator.SetTrigger("stoptMove");
    }

    public void SetEnemyTarget(EnemyController enemy) 
    {
        enemyTarget = enemy;
    }

    private void Attack() 
    {
        enemyTarget.SetDamage(damage);
    }
}
