using UnityEngine;
public class MainManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    void Start()
    {
        // Set FPS = 60
        Application.targetFrameRate = 60;
    }
    
}
