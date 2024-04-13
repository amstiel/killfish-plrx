using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldController : MonoBehaviour
{
    private enum GameState {
        Start,
        Moving,
        Fight
    }

    public UnityEvent startMove;
    public UnityEvent endMove;
    public float stepTime = 0.5f;
    public float trashHold = 0.1f;
    public bool isMoving {
        get{ return _timer > 0.0f; }
    }

    private float _timer = 0.0f;
    private GameState _currentState = GameState.Start;

    // Start is called before the first frame update
    void Start()
    {
        _currentState = GameState.Moving;
    }

    void StartUpdate() { 
    }

    void MoveUpdate() {
        bool oldIsMoving = isMoving;
        bool spacePressed = Input.GetKeyDown("space");
        _timer = Math.Max(0, _timer - Time.deltaTime);

        if (_timer < trashHold && spacePressed) {
            _timer = stepTime;
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
    
    }
    // Update is called once per frame
    void Update()
    {
        switch (_currentState) {
            case GameState.Start:
                StartUpdate();
                break;
            case GameState.Moving:
                MoveUpdate();
                break;
            case GameState.Fight:
                FigthUpdate();
                break;
        }
    }
}
