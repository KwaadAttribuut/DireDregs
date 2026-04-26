using UnityEngine;

public class tutorialHintController : MonoBehaviour
{
    private Transform player;
    [SerializeField] Canvas hintCanvas;
    [SerializeField] private float _playerAwarenessDistance;

    [System.Obsolete]
    void Awake()
    {
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            player = playerHealth.transform;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hintCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToPlayerVector = player.position - transform.position;
            if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
            {
                hintCanvas.gameObject.SetActive(true);
            }
            else
            {
                hintCanvas.gameObject.SetActive(false);
            }
    }
}
