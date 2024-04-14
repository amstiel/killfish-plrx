using UnityEngine;

public class Hetch : MonoBehaviour
{
    public WorldController controller;


    public void StartGame() 
    {
        WorldInfo.Instance().SetState(WorldInfo.GameState.Moving);
    }
}
