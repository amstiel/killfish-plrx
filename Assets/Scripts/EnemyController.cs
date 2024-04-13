using UnityEngine;

public class EnemyController : Controller
{
   
    [SerializeField] private float cooldown;
    [SerializeField] private int coins;
    private float timerCooldown = 0;
    bool battle = false;

    public int Coins => coins;

    public void Instance(PlayerController playerController) 
    {
        targetController = playerController; 
    }


    IEnumerator
}
