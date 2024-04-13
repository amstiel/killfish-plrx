using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private int damage;
    [SerializeField] private int coins;
    private Animator animator;

    public int Hp => hp;
    public int Damage => damage;
    public int Coins => coins;

    public void Awake() 
    {
        animator = GetComponent<Animator>();
    }
}
