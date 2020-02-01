using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyObj());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}