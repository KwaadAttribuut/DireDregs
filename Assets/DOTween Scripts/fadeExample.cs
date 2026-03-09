using UnityEngine;
using DG.Tweening;

public class fadeExample : MonoBehaviour
{
    SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr =  GetComponent<SpriteRenderer>();
        sr.DOFade(0,1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
