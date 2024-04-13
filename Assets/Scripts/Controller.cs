using UnityEngine;
using UnityEngine.Events;

public class Controller : MonoBehaviour
{
    [SerializeField] protected int hp;
    [SerializeField] protected int damage;
    public UnityEvent deadEvent;
    protected Animator animator;
    protected Controller targetController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Attack()
    {
        animator.SetTrigger("attack");
        targetController.SetDamage(damage);
    }

    public virtual void SetTargetController(Controller controller)
    {
        targetController = controller;
    }

    public virtual void EndBattle()
    {
        targetController = null;
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
        deadEvent.Invoke();
        deadEvent.RemoveAllListeners();
    }
}