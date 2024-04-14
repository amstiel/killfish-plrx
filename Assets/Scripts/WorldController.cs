using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldController : MonoBehaviour
{
    public UnityEvent startMove;
    public UnityEvent endMove;
    public float stepTime = 0.5f;
    public float trashHold = 0.1f;
    public bool isMoving {
        get{ return _timer > 0.0f; }
    }

    private float _timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        WorldInfo.Instance().SetState(WorldInfo.GameState.Moving);
    }

    void StartUpdate() { 
    }

    void MoveUpdate() {
        bool oldIsMoving = isMoving;
        bool spacePressed = Input.GetKeyDown("space");
        _timer = Math.Max(0, _timer - Time.deltaTime);

        if (_timer < trashHold && spacePressed) {
            _timer += stepTime;
            WorldInfo.Instance().IncGlobalFrame();
        }

        if (oldIsMoving != isMoving) {
            if (isMoving) {
                startMove.Invoke();
            }
            else {
                endMove.Invoke();
            }
        }
    }

    void FigthUpdate() {
        if (isMoving) {
            _timer = 0.0f;
            endMove.Invoke();
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (WorldInfo.Instance().gameState) {
            case WorldInfo.GameState.Moving:
                MoveUpdate();
                break;
            case WorldInfo.GameState.Start:
            case WorldInfo.GameState.Fight:
            case WorldInfo.GameState.Speaking:
                FigthUpdate();
                break;
        }
    }
}
