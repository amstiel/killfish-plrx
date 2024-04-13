using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneToGoName;
    private UnityEngine.Vector3 initialScale;
    private bool isScaled = false;
    // Start is called before the first frame update
    void Start()
    {
        this.initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isScaled)
        {
            transform.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 1.1f, transform.localScale.z * 1.1f);
            this.isScaled = true;
            Invoke("ResetScale", 0.15f);
            Invoke("ProceedToScene", 0.25f);
            
        }
    }

    void ResetScale()
    {
        this.isScaled = false;
        transform.localScale = initialScale;
    }

    void ProceedToScene() {
        SceneManager.LoadScene(sceneToGoName);
    }
}
