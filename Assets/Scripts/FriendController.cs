using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FriendController : CharactersController
{
   
    [SerializeField] private float cooldown;
    [SerializeField] private DialogTextRenderer textRenderer;
    public Animation deadAnim;

    public UnityEvent StartDialogue(GameObject speachRenderer) 
    {
        textRenderer.textRendererObject = speachRenderer;
        textRenderer.enabled = true;
        textRenderer.StartTextRender();
        return textRenderer.spechEndEvent;
    }
}
