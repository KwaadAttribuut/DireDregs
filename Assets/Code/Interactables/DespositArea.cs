using UnityEngine;
using System.Collections.Generic;

public class DespositArea : MonoBehaviour, iInteractable
{
    [SerializeField] List<EnemySlime> enemies = new List<EnemySlime>();
    [SerializeField] Vector2 playerRespawnPos;

    public bool CanInteract()
    {
        return GameManager.Instance.collectibleCount != 0;
    }

    [System.Obsolete]
    public void Interact()
    {
        if (GameManager.Instance.collectibleCount != 0)
        {
            GameManager.Instance.DepositCollectibles();
            UpdatePlayerRespawn();
            RespawnAllEnemies();
        }
    }

    [System.Obsolete]
    void UpdatePlayerRespawn()
    {
        Debug.Log($"Player Respawn Updated: {playerRespawnPos}");
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.UpdateSpawn(playerRespawnPos);
    }

    public void RespawnAllEnemies()
    {
        foreach (var target in enemies)
        {
            target.RespawnEnemies();
        }
    }
}
