using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private int collectionScore;
    [SerializeField] Sprite[] trashPool;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = trashPool[Random.Range(0, trashPool.Length)];
    }
    void Update()
    {
        transform.Rotate(0f, 0, rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("collectionArea"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.collectibleSFX);
            GameManager.Instance.AddCollectible(collectionScore);
            Destroy(gameObject);
        }
    }
}
