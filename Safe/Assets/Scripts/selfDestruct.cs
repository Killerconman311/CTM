using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    public float timer = 7f;

    private Coroutine _returnToPoolTimerCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            objPoolManager.ReturnObjectToPool(this.gameObject);
        
    }

    private void OnEnable()
    {
        timer = 7f;
        _returnToPoolTimerCoroutine = StartCoroutine(ReturnToPoolAfterTime());
    }
    private void OnDisable()
    {        
        StopCoroutine(_returnToPoolTimerCoroutine);
    }

    private void Update()
    {
        timer = timer - Time.deltaTime;

        if (timer <= 0)
        {
            objPoolManager.ReturnObjectToPool(this.gameObject);
        }
        
    }

    private IEnumerator ReturnToPoolAfterTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < timer)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        objPoolManager.ReturnObjectToPool(this.gameObject);
    }
    
    
}
