using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BackgeoundEntiityScriptableObject", order = 1)]
public class BackgeoundEntiity : ScriptableObject
{
    public Vector2Int frames = new Vector2Int(-1, -1);
    public int chance = 10;
    public bool isSpesial = false;
    public GameObject prefab;
    public Sprite sprite;
}
