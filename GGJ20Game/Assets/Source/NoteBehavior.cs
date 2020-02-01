using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    public GameObject Manager;
    public float Speed;
    public int Key;

    private KeyCode key;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0, -Speed, 0);
    }
}
