using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStaticNode : BackgroundNode
{
    public float speed = 0.1f;
    public GameObject backgroundPrefab;
    [HideInInspector]
    public Camera mainCamera;

    private List<Renderer> _backgroundSprites = new List<Renderer>();
    private bool _scrolling = false;
    private int _currentBackgroundIndex = 0;
    private Plane[] _cameraPlane;

    // Start is called before the first frame update
    void Start()
    {
    }

    bool IsRenderOnScreen(Renderer renderer) {
        return GeometryUtility.TestPlanesAABB(_cameraPlane, renderer.bounds);
    }

    private void CheckMove() {
        while (!IsRenderOnScreen(_backgroundSprites[_currentBackgroundIndex])) 
        {
            Vector3 nextRendererPosition = _backgroundSprites[(_currentBackgroundIndex + _backgroundSprites.Count - 1) % _backgroundSprites.Count].transform.position;
            Renderer prevRenderer = _backgroundSprites[_backgroundSprites.Count - 1];
            _backgroundSprites[_currentBackgroundIndex].transform.position = nextRendererPosition + new Vector3(prevRenderer.bounds.size.x, 0.0f, 0.0f);
            _currentBackgroundIndex = (_currentBackgroundIndex + 1) % _backgroundSprites.Count;
        }
    }

    public override void SetCamera(Camera camera) {
        mainCamera = camera;
        _cameraPlane = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        CheckAdd();
    }

    private void CheckAdd() {
        while (_backgroundSprites.Count == 0 || IsRenderOnScreen(_backgroundSprites[_backgroundSprites.Count - 1])) {
            if (_backgroundSprites.Count == 0) {
                _backgroundSprites.Add(Instantiate(backgroundPrefab, transform).GetComponent<Renderer>());
            }
            else {
                _backgroundSprites.Add(Instantiate(backgroundPrefab, _backgroundSprites[_backgroundSprites.Count - 1].transform.position +
                    new Vector3(_backgroundSprites[_backgroundSprites.Count - 1].bounds.size.x, 0.0f, 0.0f), new Quaternion(), transform).GetComponent<Renderer>());
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!_scrolling) { 
            return; 
        }

        foreach (Renderer sprite in _backgroundSprites) {
            sprite.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }

        CheckMove();
    }
    public override void StartScroll() {
        _scrolling = true;
    }

    public override void StopScroll() {
        _scrolling = false;
    }
}
