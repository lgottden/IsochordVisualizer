using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        void NotesToPlay(List<MPTKEvent> mptkEvents)
        {
            foreach (MPTKEvent mptkEvent in mptkEvents)
            {
                if (mptkEvent.Command == MPTKCommand.NoteOn)
                {
                    Debug.Log(
                        $"Note on Time:{mptkEvent.RealTime} millisecond " +
                        $"Note:{mptkEvent.Value}  Duration:{mptkEvent.Duration}" +
                        $"millisecond  Velocity:{mptkEvent.Velocity}");

                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.localScale -= new Vector3(0.6f, 0.6f, 0.6f);
                    var sphereRenderer = sphere.GetComponent<Renderer>();
                    sphereRenderer.material.SetColor("_Color", Color.blue);

                    switch (mptkEvent.Value % 12)
                    {
                        case 0:
                            sphere.transform.position = vertices[6];
                            break;
                        case 1:
                            sphere.transform.position = vertices[1];
                            break;
                        case 2:
                            sphere.transform.position = vertices[8];
                            break;
                        case 3:
                            sphere.transform.position = vertices[3];
                            break;
                        case 4:
                            sphere.transform.position = vertices[10];
                            break;
                        case 5:
                            sphere.transform.position = vertices[5];
                            break;
                        case 6:
                            sphere.transform.position = vertices[20];
                            break;
                        case 7:
                            sphere.transform.position = vertices[7];
                            break;
                        case 8:
                            sphere.transform.position = vertices[2];
                            break;
                        case 9:
                            sphere.transform.position = vertices[9];
                            break;
                        case 10:
                            sphere.transform.position = vertices[4];
                            break;
                        case 11:
                            sphere.transform.position = vertices[11];
                            break;
                    }

                    Destroy(sphere, mptkEvent.Duration / 1000f);
                }
            }
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
    
    void Start()
    {
        Generate();
    }
}