using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(GetComponent<Rigidbody2D>());
        this.transform.position = new Vector3(69.96564f, 87.86626f, -97.6301f);
        this.transform.eulerAngles = new Vector3(-0.47f, 65.785f, -85.48801f);
        this.transform.localScale = new Vector3(1, 1.5f, 0.85f);
        //gameObject.AddComponent<Rigidbody2D>();
    }
}
