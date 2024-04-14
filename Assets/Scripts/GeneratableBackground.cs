using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GeneratableBackground : BackgroundNode
{
    public List<BackgeoundEntiity> _configs = new List<BackgeoundEntiity>();
    public BackgeoundEntiity empty;
    public float speed = 1.0f;
    public int counter = 0;

    private List<Tuple<string, SpriteRenderer>> _entities = new List<Tuple<string, SpriteRenderer>>();
    private Dictionary<string, List<SpriteRenderer>> _invisibleEntities = new Dictionary<string, List<SpriteRenderer>>();

    private bool _scrolling = false;
    private Camera mainCamera;
    private Plane[] _cameraPlane;

    // Start is called before the first frame update
    void Start()
    {
    }

    bool IsRenderOnScreen(SpriteRenderer SpriteRenderer)
    {
        return GeometryUtility.TestPlanesAABB(_cameraPlane, SpriteRenderer.bounds);
    }
    private void CheckAdd()
    {
        int maxRand = 0;
        var currentConfigs = new List<BackgeoundEntiity>();
        foreach (BackgeoundEntiity config in _configs)
        {
            if (!config.isUsed && (config.frames[0] == -1 || config.frames[0] <= counter) &&
                (config.frames[1] == -1 || config.frames[1] >= counter))
            {
                if (config.isSpesial)
                {
                    currentConfigs.Clear();
                    currentConfigs.Add(config);
                    maxRand = 0;
                    break;
                }
                currentConfigs.Add(config);
                maxRand += config.chance;
            }
        }

        if (currentConfigs.Count == 0)
        {
            currentConfigs.Add(empty);
        }

        while (_entities.Count == 0 || IsRenderOnScreen(_entities[_entities.Count - 1].Item2))
        {
            counter++;
            int rand = UnityEngine.Random.Range(0, maxRand);
            int index = 0;
            for (; (index < currentConfigs.Count - 1) && currentConfigs[index].chance < rand; index++, rand -= currentConfigs[index].chance) ;

            if (currentConfigs[index].isSpesial) {
                currentConfigs[index].isUsed = true;
            }

            if (_entities.Count == 0)
            {
                _entities.Add(new Tuple<string, SpriteRenderer>(currentConfigs[index].name, Instantiate(currentConfigs[index].prefab, transform).GetComponent<SpriteRenderer>()));
                _entities[0].Item2.sprite = currentConfigs[index].sprite;
                continue;
            }

            SpriteRenderer obj = null;
            if (_invisibleEntities.ContainsKey(currentConfigs[index].name) && _invisibleEntities[currentConfigs[index].name].Count != 0)
            {
                obj = _invisibleEntities[currentConfigs[index].name][0];
                _invisibleEntities[currentConfigs[index].name].RemoveAt(0);
            }
            else
            {
                obj = Instantiate(currentConfigs[index].prefab, transform).GetComponent<SpriteRenderer>();
                obj.sprite = currentConfigs[index].sprite;
            }

            obj.transform.position = _entities[_entities.Count - 1].Item2.transform.position +
                    new Vector3(_entities[_entities.Count - 1].Item2.bounds.size.x / 2.0f + obj.bounds.size.x / 2, 0.0f, 0.0f);
            _entities.Add(new Tuple<string, SpriteRenderer>(currentConfigs[index].name, obj));
        }
    }

    private void CheckRemove()
    {
        if (_entities.Count == 0)
        {
            return;
        }

        while (!IsRenderOnScreen(_entities[0].Item2))
        {
            if (!_invisibleEntities.ContainsKey(_entities[0].Item1))
            {
                _invisibleEntities.Add(_entities[0].Item1, new List<SpriteRenderer>());
            }
            _invisibleEntities[_entities[0].Item1].Add(_entities[0].Item2);
            _entities.RemoveAt(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_scrolling)
        {
            return;
        }

        foreach (var SpriteRenderer in _entities)
        {
            SpriteRenderer.Item2.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }

        CheckAdd();
        CheckRemove();
    }

    public override void StartScroll()
    {
        _scrolling = true;
    }

    public override void StopScroll()
    {
        _scrolling = false;
    }
    public override void SetCamera(Camera camera)
    {
        mainCamera = camera;
        _cameraPlane = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        CheckAdd();
        counter = 0;
    }
}
