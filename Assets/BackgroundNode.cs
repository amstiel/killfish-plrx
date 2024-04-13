using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BackgroundNode : MonoBehaviour
{
    public float speed = 0.1f;
    public GameObject backgroundPrefab;
    [HideInInspector]
    public Camera mainCamera;

    private List<GameObject> _backgroundSprites = new List<GameObject>();
    private bool _scrolling = false;
    private int _currentBackgroundIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _backgroundSprites.Add(Instantiate(backgroundPrefab, transform));
        CheckAdd();
    }

    bool IsRenderOnScreen(Renderer renderer) {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(mainCamera), renderer.bounds);
    }

    private void CheckMove() {
        while (!IsRenderOnScreen(_backgroundSprites[_currentBackgroundIndex].GetComponent<Renderer>())) {
            _backgroundSprites[_currentBackgroundIndex].transform.position = 
                _backgroundSprites[(_currentBackgroundIndex + _backgroundSprites.Count - 1) % _backgroundSprites.Count].transform.position +
                new Vector3(_backgroundSprites[_backgroundSprites.Count - 1].GetComponent<Renderer>().bounds.size.x, 0.0f, 0.0f);
            _currentBackgroundIndex = (_currentBackgroundIndex + 1) % _backgroundSprites.Count;
        }
    }

    public void SetCamera(Camera camera) {
        mainCamera = camera;
    }

    private void CheckAdd() {
        while (IsRenderOnScreen(_backgroundSprites[_backgroundSprites.Count - 1].GetComponent<Renderer>())) {
            _backgroundSprites.Add(Instantiate(backgroundPrefab, _backgroundSprites[_backgroundSprites.Count - 1].transform.position +
                new Vector3(_backgroundSprites[_backgroundSprites.Count - 1].GetComponent<Renderer>().bounds.size.x, 0.0f, 0.0f), new Quaternion(), transform));
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!_scrolling) { 
            return; 
        }

        foreach (GameObject sprite in _backgroundSprites) {
            sprite.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }

        CheckMove();
    }
    public void StartScroll() {
        _scrolling = true;
    }

    public void StopScroll() {
        _scrolling = false;
    }
}
