using UnityEngine;
using DG.Tweening;

public class AmmoCollectible : MonoBehaviour
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
