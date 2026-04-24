using Unity.Cinemachine;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    [SerializeField] PolygonCollider2D startingBoundary;
    [SerializeField] PolygonCollider2D[] respawnBoundary;
    [SerializeField] PolygonCollider2D currentBoundary;
    CinemachineConfiner2D confiner;

    [System.Obsolete]
    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner2D>();
    }

    void Start()
    {
        currentBoundary = startingBoundary;
        confiner.BoundingShape2D = startingBoundary;
    }

    public void SetNewRespawnBoundary(int checkpointNo)
    {
        confiner.BoundingShape2D = respawnBoundary[checkpointNo];
        currentBoundary = respawnBoundary[checkpointNo];
    }

    public void setToBoundary()
    {
        confiner.BoundingShape2D = currentBoundary;
    }
}
