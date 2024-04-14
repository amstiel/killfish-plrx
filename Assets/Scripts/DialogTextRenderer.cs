using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogTextRenderer : MonoBehaviour
{
    public string textToRender;
    public string finalTextToRender;
    public float textSpeed = 0.1f;
    public int maxCharsInBubble = 42;
    public UnityEvent spechEndEvent;

    public Vector2 offset = new Vector2(-125, 100);

    public GameObject textRendererObject;

    private Text textComponent;

    private bool isCurrentChunkRendered = false;

    private List<string> textChunks = new List<string>();
    
    private int currentChunkIndex = 0;

    private IEnumerator RenderText(string chunkToRender){
        textComponent.text = "";
        for (int i = 0; i < chunkToRender.Length; i++){
            textComponent.text += chunkToRender[i];
            yield return new WaitForSeconds(textSpeed);
        }
        textComponent.text += " ▼";
        isCurrentChunkRendered = true;
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("DialogTextRenderer started");
       
    }

    // Update is called once per frame
    void Update()
    {
        this.textRendererObject.transform.position = ConvertWorldPositionToCanvasPosition(this.transform.position) + offset;


        if (Input.GetKeyDown(KeyCode.Space) && isCurrentChunkRendered){
            if (currentChunkIndex < textChunks.Count - 1){
                currentChunkIndex++;
                isCurrentChunkRendered = false;
                StartCoroutine(RenderText(textChunks[currentChunkIndex]));
            } else {
                Debug.Log("End of speach");
                spechEndEvent.Invoke();
            }
        }
    }

    void splitTextToChunks() {
        for (int i = 0; i < textToRender.Length; i += maxCharsInBubble) {
            string chunk = textToRender.Substring(i, Mathf.Min(maxCharsInBubble, textToRender.Length - i));
            
            if (chunk[chunk.Length - 1] != ' ' && i + maxCharsInBubble < textToRender.Length){
                int lastSpace = chunk.LastIndexOf(' ');
                chunk = chunk.Substring(0, lastSpace);
                i -= maxCharsInBubble - lastSpace;
            }
            
            textChunks.Add(chunk);
        }
    }

    Vector2 ConvertWorldPositionToCanvasPosition(Vector3 worldPosition)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        return screenPosition;
    }

    public void StartTextRender() {
        splitTextToChunks();
        currentChunkIndex = 0;
        isCurrentChunkRendered = false;
        textComponent = textRendererObject.GetComponentInChildren<Text>();
        StartCoroutine(RenderText(textChunks[0]));
    }

    public void StartFinalText(){
        textToRender = finalTextToRender;
        StartTextRender();
    }
}
