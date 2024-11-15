using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using System;
using MidiPlayerTK;
using MPTK.NAudio.Midi;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CreateGrid : MonoBehaviour
{
    MidiFilePlayer midiFilePlayer;
    
    private Vector3[] vertices;
    private Mesh mesh;

    public void Generate()
    {
        midiFilePlayer = FindObjectOfType<MidiFilePlayer>();
        midiFilePlayer.OnEventNotesMidi.AddListener(NotesToPlay);
        
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        
        vertices = new Vector3[12 * 6];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        

        void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            GameObject line = new GameObject();
            line.transform.position = start;
            line.AddComponent<LineRenderer>();
            LineRenderer lr = line.GetComponent<LineRenderer>();
            lr.material.SetColor("_Color", color);
            lr.SetWidth(0.1f, 0.1f);
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
        }
        
        for (int i = 0, x = 0; i < 12; i++, x++)
        {
            vertices[i] = new Vector3(x, 0);
            vertices[i + 12] = new Vector3(x + 0.5f, 1);
        }
        for (int i = 24, x = 0; i < 36; i++, x++)
        {
            vertices[i] = new Vector3(x, 2);
            vertices[i + 12] = new Vector3(x + 0.5f, 3);
        }
        for (int i = 48, x = 0; i < 60; i++, x++)
        {
            vertices[i] = new Vector3(x, 4);
            vertices[i + 12] = new Vector3(x + 0.5f, 5);
        }
                 
        mesh.vertices = vertices;
        mesh.uv = uv; 
        mesh.tangents = tangents;

        int[] triangles = new int[12 * 6 * 6];

        /*
        void DrawRoot(int root)
        {
            sphere.transform.position = vertices[root];
        }
        //DrawRoot(30);

        void DrawPerfect5th(int root)
        {
            sphere.transform.position = vertices[root];
            DrawLine(vertices[root], vertices[root + 1], Color.black);
        }
        //DrawPerfect5th(30);

        void DrawMajor2nd(int root)
        {
            sphere.transform.position = vertices[root];
            DrawLine(vertices[root], vertices[root + 2], Color.black);
            triangles[0] = root + 10;
            triangles[1] = triangles[5] = root + 11;
            triangles[2] = triangles[4] = root - 1;
            triangles[3] = root;
        }
        //DrawMajor2nd(30);

        void DrawMajor6th(int root)
        {
            sphere.transform.position = vertices[root];
            DrawLine(vertices[root], vertices[root + 11], Color.black);
        }
        //DrawMajor6th(30);

        void DrawMajor3rd(int root)
        {
            sphere.transform.position = vertices[root];
            DrawLine(vertices[root], vertices[root + 12], Color.black);
        }
        //DrawMajor3rd(30);
        
        void DrawMajor7th(int root)
        {
            sphere.transform.position = vertices[root];
            triangles[0] = root - 24;
            triangles[1] = triangles[5] = root - 13;
            triangles[2] = triangles[4] = root - 12;
            triangles[3] = triangles[8] = root;
            triangles[7] = triangles[9] = root + 1;
            triangles[6] = triangles[10] = root + 12;
            triangles[11] = root + 13;
        }
        //DrawMajor7th(30);

        void DrawTritone(int root)
        {
            sphere.transform.position = vertices[root];
            DrawLine(vertices[root + 23], vertices[root - 23], Color.black);
        }
        //DrawTritone(30);

        void DrawMinor2nd(int root)
        {
            sphere.transform.position = vertices[root];
            triangles[0] = root - 14;
            triangles[1] = triangles[5] = root - 1;
            triangles[2] = triangles[4] = root - 13;
            triangles[3] = triangles[8] = root;
            triangles[7] = triangles[9] = root + 12;
            triangles[6] = triangles[10] = root + 11;
            triangles[11] = root + 24;
        }
        //DrawMinor2nd(30);

        void DrawMinor6th(int root)
        {
            sphere.transform.position = vertices[root];
            DrawLine(vertices[root], vertices[root - 13], Color.black);
        }
        //DrawMinor6th(30);
        
        void DrawMinor3rd(int root)
        {
            sphere.transform.position = vertices[root];
            DrawLine(vertices[root], vertices[root - 12], Color.black);
        }
        //DrawMinor3rd(30);

        void DrawMinor7th(int root)
        {
            sphere.transform.position = vertices[root];
            DrawLine(vertices[root], vertices[root - 2], Color.black);
            triangles[0] = root - 11;
            triangles[1] = triangles[5] = root - 12;
            triangles[2] = triangles[4] = root + 1;
            triangles[3] = root;
        }
        //DrawMinor7th(30);

        void DrawPerfect4th(int root)
        {
            sphere.transform.position = vertices[root];
            DrawLine(vertices[root], vertices[root - 1], Color.black);
        }
        //DrawPerfect4th(30);
        */
        
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        void NotesToPlay(List<MPTKEvent> mptkEvents)
        {
            //Debug.Log("Received " + mptkEvents.Count + " MIDI Events");

            foreach (MPTKEvent mptkEvent in mptkEvents)
            {
                if (mptkEvent.Command == MPTKCommand.NoteOn)
                {
                    Debug.Log($"Note on Time:{mptkEvent.RealTime} millisecond  Note:{mptkEvent.Value}  Duration:{mptkEvent.Duration} millisecond  Velocity:{mptkEvent.Velocity}");
                    
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.localScale -= new Vector3(0.6f, 0.6f, 0.6f);
                    var sphereRenderer = sphere.GetComponent<Renderer>();
                    sphereRenderer.material.SetColor("_Color", Color.blue);

                    if (mptkEvent.Value % 12 == 0)
                    {
                        sphere.transform.position = vertices[6];
                    }
                    if (mptkEvent.Value % 12 == 1)
                    {
                        sphere.transform.position = vertices[1];
                    }
                    if (mptkEvent.Value % 12 == 2)
                    {
                        sphere.transform.position = vertices[8];
                    }
                    if (mptkEvent.Value % 12 == 3)
                    {
                        sphere.transform.position = vertices[3];
                    }
                    if (mptkEvent.Value % 12 == 4)
                    {
                        sphere.transform.position = vertices[10];
                    }
                    if (mptkEvent.Value % 12 == 5)
                    {
                        sphere.transform.position = vertices[5];
                    }
                    if (mptkEvent.Value % 12 == 6)
                    {
                        sphere.transform.position = vertices[20];
                    }
                    if (mptkEvent.Value % 12 == 7)
                    {
                        sphere.transform.position = vertices[7];
                    }
                    if (mptkEvent.Value % 12 == 8)
                    {
                        sphere.transform.position = vertices[2];
                    }
                    if (mptkEvent.Value % 12 == 9)
                    {
                        sphere.transform.position = vertices[9];
                    }
                    if (mptkEvent.Value % 12 == 10)
                    {
                        sphere.transform.position = vertices[4];
                    }
                    if (mptkEvent.Value % 12 == 11)
                    {
                        sphere.transform.position = vertices[11];
                    }
                    
                    //DoDelayAction(mptkEvent.Duration);
                    Destroy(sphere, mptkEvent.Duration / 1000f);
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

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.white;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
    
    private void Start()
    {
        Generate();
    }
}