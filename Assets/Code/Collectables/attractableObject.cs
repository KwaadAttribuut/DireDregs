using UnityEngine;

public class attractableObject : MonoBehaviour
{
    private GameObject _myPlayer;
    [SerializeField] private float rotationSpeed = 90f;
    private Rigidbody2D rb;
    void Start()
    {
        _myPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("suctionArea"))
        {
            transform.Rotate(0f, 0, rotationSpeed * Time.deltaTime);
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector3.Lerp(this.transform.position, _myPlayer.transform.position, 3f * Time.deltaTime);
    }
}
