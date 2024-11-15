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
        //Debug.Log("Received " + mptkEvents.Count + " MIDI Events");

        foreach (MPTKEvent mptkEvent in mptkEvents)
        {
            if (mptkEvent.Command == MPTKCommand.NoteOn)
            {
                Debug.Log($"Note on Time:{mptkEvent.RealTime} millisecond  Note:{mptkEvent.Value}  Duration:{mptkEvent.Duration} millisecond  Velocity:{mptkEvent.Velocity}");
                DoDelayAction(mptkEvent.Duration);
            }
            
            // Debug.Log(mptkEvent.ToString());
        }
    }
    
    void DoDelayAction(float delayTime)
    {
        StartCoroutine(DelayAction(delayTime));
    }

    IEnumerator DelayAction(float delayTime)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);

        //Do the action after the delay time has finished.
    }
}

