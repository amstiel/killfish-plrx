using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BackgroundNode: MonoBehaviour {
    public abstract void SetCamera(Camera camera);
    public abstract void StartScroll();
    public abstract void StopScroll();
}

public class BackgroudScroller : MonoBehaviour
{
    public List<BackgroundNode> backgroundNodes = new List<BackgroundNode>();
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        foreach (BackgroundNode node in backgroundNodes) {
            node.SetCamera(mainCamera);
        }
    }

    public void StartScroll() {
        foreach (BackgroundNode node in backgroundNodes) {
            node.StartScroll();
        }
    }

    public void StopScroll() {
        foreach (BackgroundNode node in backgroundNodes) {
            node.StopScroll();
        }
    }
}
