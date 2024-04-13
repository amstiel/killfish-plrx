using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] float secondsToDestroy;
    private void Awake()
    {
        Destroy(this, secondsToDestroy);
    }
}
