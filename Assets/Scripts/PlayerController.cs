using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveTime;
    private Animator animator; 

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Move() 
    {
        animator.SetTrigger("move");
    }
}
