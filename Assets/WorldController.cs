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
    public bool isMoving {
        get{ return timer > 0.0f; }
    }

    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool oldIsMoving = isMoving;
        bool spacePressed = Input.GetKeyDown("space");
        timer = Math.Max(0, timer - Time.deltaTime);

        if (timer == 0.0f && spacePressed) {
            timer = stepTime;
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
}
