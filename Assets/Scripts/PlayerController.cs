using UnityEngine;

public class PlayerController : Controller
{
    [SerializeField] private float moveTime;


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
        animator.SetTrigger("stopMove");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && targetController != null) 
        {
            Attack();
        }
    }
}
