using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemySO : ScriptableObject
{
    [SerializeField] private int hp;
    [SerializeField] private int damage;
    [SerializeField] private int coins;
    [SerializeField] private GameObject graphicPref;

    public int Hp => hp;
    public int Damage => damage;
    public int Coins => coins;
    public GameObject GraphicPref => graphicPref;

}
