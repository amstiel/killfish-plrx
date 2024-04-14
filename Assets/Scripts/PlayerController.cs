using UnityEngine;
using UnityEngine.UI;

public class PlayerController : CharactersController
{
    [SerializeField] private hpUI hpUI;
    [SerializeField] private Text counterDamage;
    [SerializeField] private Text counterArmor;
    [SerializeField] private Text counterCoins;

    private void Start()
    {
        UpdateUI();
    }

    public override void ReceiveDamage(int damage)
    {
        base.ReceiveDamage(damage);
        hpUI.SetHp(hp, maxHp);
    }

    public void StartMove() 
    {
        animator.SetTrigger("startMove");
    }
    public void StopMove()
    {
        animator.SetTrigger("stopMove");
    }

    public void HealHp(int hp) 
    {
        this.hp+= hp;
        if (this.hp > maxHp) 
        {
            this.hp = maxHp;
        }
        UpdateUI();
    }

    public void AddMaxHp(int maxHp) 
    {
        this.maxHp+= maxHp;
        UpdateUI();
    }

    public void AddDamage(int damage) 
    {
        this.damage+= damage;
        UpdateUI();
    }

    public void AddArmor(int armor) 
    {
        this.armor+= armor;
        UpdateUI();
    }

    public void AddCoins(int coins) 
    {
        this.coins+= coins;
        UpdateUI();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && targetController != null) 
        {
            Attack();
        }
    }

    private void UpdateUI() 
    {
        counterDamage.text = damage.ToString();
        hpUI.SetHp(hp, maxHp);
        counterArmor.text = armor.ToString();
        counterCoins.text = coins.ToString();
    }
}
