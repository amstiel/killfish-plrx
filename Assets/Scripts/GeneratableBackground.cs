using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class GeneratableBackground : BackgroundNode {
    public List<BackgeoundEntiity> _configs = new List<BackgeoundEntiity>();
    public float speed = 1.0f;

    private List<Tuple<string, Renderer>> _entities = new List<Tuple<string, Renderer>>();
    private Dictionary<string, List<Renderer>> _invisibleEntities = new Dictionary<string, List<Renderer>>();

    private bool _scrolling = false;
    private Camera mainCamera;
    private Plane[] _cameraPlane;
    private int maxRand = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (BackgeoundEntiity config in _configs) {
            maxRand += config.chance;
        }

        CheckAdd();
    }

    bool IsRenderOnScreen(Renderer renderer) {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(mainCamera), renderer.bounds);
    }
    private void CheckAdd() {
        while (_entities.Count == 0 || IsRenderOnScreen(_entities[_entities.Count - 1].Item2) ) {
            int rand = UnityEngine.Random.Range(0, maxRand);
            int index = 0;
            for (;_configs[index].chance < rand; index++, rand -= _configs[index].chance);

            if (_entities.Count == 0) {
                _entities.Add(new Tuple<string, Renderer>(_configs[index].prefab.name, Instantiate(_configs[index].prefab, transform).GetComponent<Renderer>()));
                continue;
            }

            Renderer obj = null;
                if (_invisibleEntities.ContainsKey(_configs[index].prefab.name) && _invisibleEntities[_configs[index].prefab.name].Count != 0) {
                obj = _invisibleEntities[_configs[index].prefab.name][0];
                _invisibleEntities[_configs[index].prefab.name].RemoveAt(0);
            }
            else {
                 obj = Instantiate(_configs[index].prefab, transform).GetComponent<Renderer>();
            }

            obj.transform.position = _entities[_entities.Count - 1].Item2.transform.position +
                    new Vector3(_entities[_entities.Count - 1].Item2.bounds.size.x / 2.0f + obj.bounds.size.x / 2, 0.0f, 0.0f);
            _entities.Add(new Tuple<string, Renderer>(_configs[index].prefab.name, obj));
        }
    }

    private void CheckRemove() {
        while (!IsRenderOnScreen(_entities[0].Item2)) {
             if (!_invisibleEntities.ContainsKey(_entities[0].Item1)) {
                _invisibleEntities.Add(_entities[0].Item1, new List<Renderer>());
            }
            _invisibleEntities[_entities[0].Item1].Add(_entities[0].Item2);
            _entities.RemoveAt(0);
        }
    }

    // Update is called once per frame
    void Update() {
        if (!_scrolling) {
            return;
        }

        foreach (var renderer in _entities) {
            renderer.Item2.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }

        CheckAdd();
        CheckRemove();
    }

    public override void StartScroll() {
        _scrolling = true;
    }

    public override void StopScroll() {
        _scrolling = false;
    }
    public override void SetCamera(Camera camera) {
        mainCamera = camera;
        _cameraPlane = GeometryUtility.CalculateFrustumPlanes(mainCamera);
    }
}
