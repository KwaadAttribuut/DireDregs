using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // UI display elements would be in a seperate UI script in order to avoid the game manager becoming a God Object
    public static GameManager Instance {get; private set;}
    public int collectibleCount = 0;
    public TMP_Text collectibleText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        collectibleText.text = $"Collectibles: {collectibleCount}"; // lazy line, as mentioned it should be in its own UI script
    }

    public void AddCollectible()
    {
        collectibleCount++;
        UpdateCollectibleUI();
    }
    private void UpdateCollectibleUI()
    {
        if(collectibleText != null)
        {
            collectibleText.text = $"Collectibles: {collectibleCount}";
        }
    }
}
