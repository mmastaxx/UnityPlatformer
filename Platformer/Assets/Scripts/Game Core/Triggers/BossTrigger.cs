using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public static event Action ArenaEntered;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            ArenaEntered?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
