using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitCollider : MonoBehaviour
{
    public static event Action ExitEntered;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("Work");
        if (collision.gameObject.layer == 7)
        {
            ExitEntered?.Invoke();
        }
    }
}
