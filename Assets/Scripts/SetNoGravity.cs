using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNoGravity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
    }
}
