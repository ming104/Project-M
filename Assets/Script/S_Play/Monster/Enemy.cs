using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer _sprRanderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _sprRanderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeAttacked()
    {
        StartCoroutine(Hit());
    }

    private IEnumerator Hit()
    {
        _sprRanderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        _sprRanderer.color = Color.white;
    }
}
