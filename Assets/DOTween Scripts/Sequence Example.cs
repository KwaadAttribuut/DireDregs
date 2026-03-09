using UnityEngine;
using DG.Tweening;

public class SequenceExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMoveX(3,1f));
        seq.Append(transform.DOMoveY(2,1f));
        seq.Append(transform.DOMoveZ(2,.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
