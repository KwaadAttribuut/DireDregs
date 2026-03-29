using UnityEngine;

public class AmmoCollectible : MonoBehaviour
{
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("collectionArea"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.collectibleSFX);
            GameManager.Instance.AddAmmo(2);
            Destroy(gameObject);
        }
    }
}
