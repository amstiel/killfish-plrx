using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : CharactersController
{
    [SerializeField] private hpUI hpUI;
    [SerializeField] private Text counterDamage;
    [SerializeField] private Text counterArmor;
    [SerializeField] private Text counterCoins;
    [SerializeField] private Animator DeadAnim;
    bool isDead = false;
    float deadTime = 0;

    private void Start()
    {
        UpdateUI();
    }

    override protected void Dead() {
        base.Dead();
        isDead = true;
        deadTime = 2.0f;
        DeadAnim.SetTrigger("Dead");
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
        if (deadTime > 0.0f) { 
            deadTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isDead && deadTime <= 0.0f) {
            SceneManager.LoadScene("MainMenu");
            return;
        }

        if (isDead) {
            return;
        }

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
