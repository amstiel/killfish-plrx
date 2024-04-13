using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] protected int hp;
    [SerializeField] protected int damage;
    protected WorldInfo worldInfo;
    protected Animator animator;
    protected Controller targetController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        worldInfo = WorldInfo.Instance();
    }

    protected virtual void Attack()
    {
        animator.SetTrigger("Attack");
        targetController.SetDamage(damage);
    }

    protected void SetTargetController(Controller controller) 
    {
        targetController = controller;
    }

    public void SetDamage(int damage)
    {
        hp -= damage;

        if (hp < 0)
        {
            hp = 0;
            Dead();
        }
    }

    protected void Dead()
    {

    }
}