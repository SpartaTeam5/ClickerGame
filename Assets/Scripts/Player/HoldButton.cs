using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button targetbtn;
    public float holdInterval = 0.2f;

    private bool isHolding = false;
    private Coroutine holdCoroutine;

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        holdCoroutine = StartCoroutine(DelayHoldRoutine());
    }

    public void OnPointerUp(PointerEventData eventData) 
    { 
        isHolding=false;
        if (holdCoroutine != null) 
            StopCoroutine(holdCoroutine);
    }

    IEnumerator DelayHoldRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(HoldRoutine());
    }
    IEnumerator HoldRoutine()
    {
        while (isHolding)
        {
            targetbtn.onClick.Invoke();
            yield return new WaitForSeconds(holdInterval);
        }
    }
}
