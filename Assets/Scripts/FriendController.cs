using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FriendController : CharactersController
{
   
    [SerializeField] private DialogTextRenderer textRenderer;
    [SerializeField] private int bonusAttack = 0;
    [SerializeField] private int bonusArmor = 0;
    [SerializeField] private int bonusHp = 0;
    [SerializeField] private int healHp = 0;
    [SerializeField] private int bonusCoins = 0;

    public UnityEvent bonusAddedEvent;

    public UnityEvent StartDialogue(GameObject speachRenderer) 
    {
        Debug.Log("Friend", speachRenderer);
        textRenderer.textRendererObject = speachRenderer;
        textRenderer.enabled = true;
        textRenderer.StartTextRender();

        return textRenderer.spechEndEvent;
    }

    public UnityEvent GiveBonusToPlayer() 
    {
        PlayerController playerController = (PlayerController)targetController;

        playerController.AddCoins(bonusCoins);
        playerController.AddMaxHp(bonusHp);
        playerController.HealHp(healHp);
        playerController.AddDamage(bonusAttack);
        playerController.AddArmor(bonusArmor);

        return bonusAddedEvent;
    }
}
