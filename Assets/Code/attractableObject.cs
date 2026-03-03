using UnityEngine;

public class attractableObject : MonoBehaviour
{
    private GameObject _myPlayer;
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
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector3.Lerp(this.transform.position, _myPlayer.transform.position, 3f * Time.deltaTime);
    }
}
