using UnityEngine;
using UnityEngine.Events;

public class Controller : MonoBehaviour
{
    [SerializeField] protected int hp;
    [SerializeField] protected int damage;
    [SerializeField] protected int armor;
    public UnityEvent deadEvent;
    protected int maxHp;
    protected Animator animator;
    protected Controller targetController;

    protected virtual void Awake()
    {
        maxHp = hp;
        animator = GetComponent<Animator>();
    }

    protected virtual void Attack()
    {
        animator.SetTrigger("attack");
        targetController.ReceiveDamage(damage);
    }

    public virtual void SetTargetController(Controller controller)
    {
        targetController = controller;
    }

    public virtual void EndBattle()
    {
        targetController = null;
    }

    public virtual void ReceiveDamage(int damage)
    {
        int newDamage = damage - armor;

        if(newDamage>0)
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