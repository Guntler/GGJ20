using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using static Song;

public class SongManager : MonoBehaviour
{
    public double LowerLimit;
    public double UpperLimit;

    public GameObject Note1;
    public GameObject Note2;
    public GameObject Note3;
    public GameObject Note4;

    public Song CurrentSong;

    private List<GameObject> notes;
    private double CurrentSongTime;
    private bool started = false;
    private int currentNote = 0;

    private Dictionary<int, KeyCode> keys;
    private Dictionary<int, GameObject> NoteBlueprints = new Dictionary<int, GameObject>();
    

    public Song ReadSong(string songFile)
    {
        using (System.IO.StreamReader file = new System.IO.StreamReader(songFile))
        {
            string line;
            Song song = new Song();
            while ((line = file.ReadLine()) != null)
            {
                var noteInfo = line.Split(' ');
                var info = new NoteInfo();
                info.Note = int.Parse(noteInfo[0], CultureInfo.InvariantCulture);
                info.Delay = double.Parse(noteInfo[1], CultureInfo.InvariantCulture);
                song.Notes.Add(info);
            }

            file.Close();

            return song;
        }

        return null;
    }

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

        keys.Add(1, KeyCode.Keypad1);
        keys.Add(2, KeyCode.Keypad2);
        keys.Add(3, KeyCode.Keypad3);
        keys.Add(4, KeyCode.Keypad4);

        NoteBlueprints.Add(1, Note1);
        NoteBlueprints.Add(2, Note2);
        NoteBlueprints.Add(3, Note3);
        NoteBlueprints.Add(4, Note4);
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
        Instantiate(NoteBlueprints[note.Note]);
    }
}
