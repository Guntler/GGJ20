using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song
{
    public struct NoteInfo
    {
        public int Note;
        public double Delay;
    }

    public List<NoteInfo> Notes = new List<NoteInfo>();
}
