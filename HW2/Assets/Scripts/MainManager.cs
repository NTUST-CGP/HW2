using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Image _loadingBarFill;
    void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void LoadScene (int sceneId) 
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }
    IEnumerator LoadSceneAsync(int sceneId) 
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        _loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            _loadingBarFill.fillAmount = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }
    }
}
