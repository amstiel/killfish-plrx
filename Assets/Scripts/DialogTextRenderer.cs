using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogTextRenderer : MonoBehaviour
{
    public string textToRender;
    public float textSpeed = 0.1f;
    public int maxCharsInBubble = 48;
    public UnityEvent spechEndEvent;

    public Vector2 offset = new Vector2(-125, 100);

    public GameObject textRendererObject;

    private Text textComponent;

    private IEnumerator RenderText(){
        List<string> textChunks = new List<string>();

        for  (int i = 0; i < textToRender.Length; i += maxCharsInBubble) {
            string chunk = textToRender.Substring(i, Mathf.Min(maxCharsInBubble, textToRender.Length - i));
            if (chunk[chunk.Length - 1] != ' ' && i + maxCharsInBubble < textToRender.Length){
                int lastSpace = chunk.LastIndexOf(' ');
                chunk = chunk.Substring(0, lastSpace);
                i -= maxCharsInBubble - lastSpace;
            }
            textChunks.Add(chunk);
        }


        for (int j = 0; j < textChunks.Count; j++){
            textComponent.text = "";
            for (int i = 0; i < textChunks[j].Length; i++){
                textComponent.text += textChunks[j][i];
                yield return new WaitForSeconds(textSpeed);
            }
            yield return null;
        }
        spechEndEvent.Invoke();
    }


    // Start is called before the first frame update
    void Start()
    {
        this.textComponent = textRendererObject.GetComponentInChildren<Text>();
        StartCoroutine(RenderText());
    }

    // Update is called once per frame
    void Update()
    {
        this.textRendererObject.transform.position = ConvertWorldPositionToCanvasPosition(this.transform.position) + offset;
    }

    Vector2 ConvertWorldPositionToCanvasPosition(Vector3 worldPosition)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        return screenPosition;
    }
}
