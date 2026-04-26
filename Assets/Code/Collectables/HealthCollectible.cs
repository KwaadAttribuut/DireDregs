using UnityEngine;
using DG.Tweening;

public class HealthCollectible : MonoBehaviour
{
    void Start()
    {
        if (gameObject != null)
        transform.DOScale(.9f,.5f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.InOutSine);
    }
    void Update()
    {
        
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("collectionArea"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.collectibleSFX);
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            if (playerHealth.currentPlayerHealth + 2 >= 5)
            {
                playerHealth.currentPlayerHealth = 5;
            }
            else
            {
                playerHealth.currentPlayerHealth += 2;
            }
            UIManager.Instance.updateHealthUI();
            Destroy(gameObject);
        }
    }
}