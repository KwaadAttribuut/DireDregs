using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChecker : MonoBehaviour
{
    private Scene currentScene;
    [SerializeField] string curSceneName;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        curSceneName = currentScene.name;
        Debug.Log($"Current scene is {currentScene.name}");
        AudioManager.Instance.CheckScene(currentScene.name);
    }
}
