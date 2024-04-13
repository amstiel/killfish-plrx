using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveTime;
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
}
