using UnityEngine;
using System.Collections.Generic;

public class DespositArea : MonoBehaviour, iInteractable
{
    [SerializeField] List<EnemySlime> enemies = new List<EnemySlime>();
    [SerializeField] Vector2 playerRespawnPos;
    [SerializeField] int RespawnPointNo;

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
            BoundaryManager boundaryManager = FindFirstObjectByType<BoundaryManager>();
            boundaryManager.SetNewRespawnBoundary(RespawnPointNo);
            RespawnAllEnemies();
            if (GameManager.Instance.collectibleCount + GameManager.Instance.depositedCollectibleCount >= GameManager.Instance.currentQuota)
            {
                GameManager.Instance.UpdateQuota();
            }
        }
    }

    [System.Obsolete]
    void UpdatePlayerRespawn()
    {
        Debug.Log($"Player Respawn Updated: {playerRespawnPos}");
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.currentPlayerHealth = playerHealth.maxPlayerHealth;
        UIManager.Instance.updateHealthUI();
        playerHealth.UpdateSpawn(playerRespawnPos);
    }

    [System.Obsolete]
    public void ResetPlayerSpawn()
    {
        Debug.Log($"Player Respawn Updated: {playerRespawnPos}");
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.UpdateSpawn(new Vector2(0, 0));
    }

    public void RespawnAllEnemies()
    {
        foreach (var target in enemies)
        {
            target.RespawnEnemies();
        }
    }
}
