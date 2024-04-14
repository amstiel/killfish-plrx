using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class PopupConfig {
    public enum PopupType {
        Damage
    }

    public string text = "";
    public Vector2 offset = Vector2.zero;
    public float duration = 1.0f;
    public Color color = UnityEngine.Color.white;
    public PopupType type = PopupType.Damage;
    public PopupConfig() { }
    public PopupConfig Text(string text) {
        this.text = text;
        return this;
    }
    public PopupConfig Offset(Vector2 offset) {
        this.offset = offset;
        return this;
    }

    public PopupConfig Duration(float duration) {
        this.duration = duration;
        return this;
    }

    public PopupConfig Color(Color color) {
        this.color = color;
        return this;
    }

    public PopupConfig Type(PopupType type) {
        this.type = type;
        return this;
    }
}


[RequireComponent(typeof(Canvas))]
public class PopupController : MonoBehaviour {
    private static PopupController instance;
    [SerializeField] private SerializableDictionary<PopupConfig.PopupType, GameObject> _prefabs = new SerializableDictionary<PopupConfig.PopupType, GameObject>();
    private Canvas _canvas;

    public static PopupController Instance() {
        return instance;
    }

    private void Awake() {
        instance = this;
        _canvas = GetComponent<Canvas>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPopup(Transform invoker, PopupConfig config) {
        var ViewportPos = Camera.main.WorldToViewportPoint(invoker.position);
        var comp = GetComponent<RectTransform>();
        var rect = comp.rect;
        var pos = new Vector3((config.offset.x + comp.rect.width * (ViewportPos.x - 0.5f)) * comp.localScale.x, 
            (config.offset.y + comp.rect.height * (ViewportPos.y - 0.5f)) * comp.localScale.y, 0.0f);
        var instance = Instantiate(_prefabs[config.type], pos, new Quaternion(), transform);
        var popup = instance.GetComponent<PopupNode>();
        popup.Init(config);

        popup.Show();
    }
}
