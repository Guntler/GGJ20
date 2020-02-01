using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public double LowerLimit;
    public double UpperLimit;

    private List<GameObject> notes;

    // Start is called before the first frame update
    void Start()
    {
        notes = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
}
