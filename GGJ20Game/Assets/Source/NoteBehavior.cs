using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    public GameObject Manager;
    public float Speed;
    public int Key;

    private double lowerLimit;
    private double upperLimit;
    private KeyCode key;
    // Start is called before the first frame update
    void Start()
    {
        lowerLimit = Manager.GetComponent<SongManager>().LowerLimit;
        upperLimit = Manager.GetComponent<SongManager>().UpperLimit;
        switch(Key)
        {
            case 1: key = KeyCode.Keypad1; break;
            case 2: key = KeyCode.Keypad2; break;
            case 3: key = KeyCode.Keypad3; break;
            case 4: key = KeyCode.Keypad4; break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0, -Speed, 0);

        if (transform.position.y < lowerLimit)
        {
            Destroy(gameObject);
        }


        if (transform.position.y < upperLimit && Input.GetKeyDown(key))
        {
            Destroy(gameObject);
            //Award points? Or maybe in manager
        }
    }
}
