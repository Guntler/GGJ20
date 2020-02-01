using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Song;

public class SongManager : MonoBehaviour
{
    public double LowerLimit;
    public double UpperLimit;

    public Song CurrentSong;

    private List<GameObject> notes;
    private double CurrentSongTime;
    private bool started = false;
    private int currentNote = 0;

    private Dictionary<int, KeyCode> keys;

    public void StartSong(Song song)
    {
        CurrentSong = song;
        CurrentSongTime = 0;
        started = true;
        currentNote = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        keys = new Dictionary<int, KeyCode>();
        notes = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (started)
        {
            CurrentSongTime += Time.fixedDeltaTime;

            List<GameObject> failNotes = new List<GameObject>();
            List<GameObject> successNotes = new List<GameObject>();
            foreach (var note in notes)
            {
                KeyCode _key = keys[note.GetComponent<NoteBehavior>().Key];
                if (note.transform.position.y < LowerLimit)
                {
                    failNotes.Add(note);
                }
                else if (Input.GetKeyDown(_key) && note.transform.position.y < UpperLimit)
                {
                    successNotes.Add(note);
                }
            }

            foreach(var note in failNotes)
            {
                Destroy(note);
                //subtract life
            }

            foreach(var note in successNotes)
            {
                Destroy(note);
                //add score
            }

            notes = notes.Except(failNotes).ToList();
            notes = notes.Except(successNotes).ToList();

            for(var i = currentNote; i < CurrentSong.Notes.Count;)
            {
                if (CurrentSong.Notes[i].Delay < CurrentSongTime)
                {
                    SpawnNote(CurrentSong.Notes[i]);
                    i++;
                    currentNote = i;
                }
                else break;
            }
        }
    }

    public void SpawnNote(NoteInfo note)
    {

    }
}
