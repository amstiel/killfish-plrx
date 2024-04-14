using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharactersController : MonoBehaviour
{

    [SerializeField] protected int hp;
    [SerializeField] protected int damage;
    [SerializeField] protected int armor;
    [SerializeField] protected int coins;
    [SerializeField] private Text textDamagePref;
    [SerializeField] private RectTransform textSpawnPoint;
    public UnityEvent deadEvent;
    protected int maxHp;
    protected Animator animator;
    protected CharactersController targetController;

    public int Coins { get => coins;}
    public int Hp { get => hp;}

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

    public virtual void SetTargetController(CharactersController controller)
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

        Text text = Instantiate(textDamagePref, textSpawnPoint);

        text.text = damage.ToString();  

        if (newDamage>0)
            hp -= damage;

        if (hp < 0)
        {
            hp = 0;
            Dead();
        }
    }

    protected virtual void Dead()
    {
        deadEvent.Invoke();
        deadEvent.RemoveAllListeners();
    }
}