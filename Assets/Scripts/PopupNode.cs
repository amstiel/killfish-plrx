using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class PopupNode : MonoBehaviour {
    private Animator _animator;
    [SerializeField] private Text _textComp;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("End")) {
            Destroy(gameObject);
        }
    }

    public void Show() {
        _animator.SetTrigger("Show");
    }

    public void Init(PopupConfig config) {
        if (_textComp != null) {
            _textComp.text = config.text;
            _textComp.color = config.color;
        }
    }
}
