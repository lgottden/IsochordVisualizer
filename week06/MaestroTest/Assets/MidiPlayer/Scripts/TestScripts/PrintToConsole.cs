using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MidiPlayerTK;
using MPTK.NAudio.Midi;
using UnityEditor;
using UnityEngine.UIElements;

public class PrintToConsole : MonoBehaviour
{
    MidiFilePlayer midiFilePlayer;
    
    void Start()
    {
        midiFilePlayer = FindObjectOfType<MidiFilePlayer>();
        midiFilePlayer.OnEventNotesMidi.AddListener(NotesToPlay);
    }

    public void NotesToPlay(List<MPTKEvent> mptkEvents)
    {
        Debug.Log("Received " + mptkEvents.Count + " MIDI Events");

        foreach (MPTKEvent mptkEvent in mptkEvents)
        {
            if (mptkEvent.Command == MPTKCommand.NoteOn)
                Debug.Log($"Note on Time:{mptkEvent.RealTime} millisecond  Note:{mptkEvent.Value}  Duration:{mptkEvent.Duration} millisecond  Velocity:{mptkEvent.Velocity}");
            
            Debug.Log(mptkEvent.ToString());
        }
    }
}

