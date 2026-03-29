using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 playerRespawnPosition = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 enemyRespawnPosition = new Vector3(27, 2, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerHealth>();
            if(player != null)
            {
                collision.transform.position = playerRespawnPosition;
            }
            /* else if (collision.CompareTag("Enemy"))
            {
                var enemy = collision.GetComponent<enemyAI>();
                if(enemy != null)
                {
                    collision.transform.position = enemyRespawnPosition;
                }
            } */
        }
    }
}
