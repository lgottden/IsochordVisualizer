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
                            if (mptkEvent.Value == 36)
                                sphere.transform.position = vertices[6];
                            if (mptkEvent.Value == 48)
                                sphere.transform.position = vertices[14];
                            if (mptkEvent.Value == 60)
                                sphere.transform.position = vertices[35];
                            if (mptkEvent.Value == 72)
                                sphere.transform.position = vertices[43];
                            if (mptkEvent.Value == 84)
                                sphere.transform.position = vertices[52];
                            if (mptkEvent.Value == 96)
                                sphere.transform.position = vertices[60];
                            break;
                        case 1:
                            if (mptkEvent.Value == 37)
                                sphere.transform.position = vertices[1];
                            if (mptkEvent.Value == 49)
                                sphere.transform.position = vertices[21];
                            if (mptkEvent.Value == 61)
                                sphere.transform.position = vertices[30];
                            if (mptkEvent.Value == 73)
                                sphere.transform.position = vertices[38];
                            if (mptkEvent.Value == 85)
                                sphere.transform.position = vertices[59];
                            if (mptkEvent.Value == 97)
                                sphere.transform.position = vertices[67];
                            break;
                        case 2:
                            if (mptkEvent.Value == 38)
                                sphere.transform.position = vertices[8];
                            if (mptkEvent.Value == 50)
                                sphere.transform.position = vertices[16];
                            if (mptkEvent.Value == 62)
                                sphere.transform.position = vertices[25];
                            if (mptkEvent.Value == 74)
                                sphere.transform.position = vertices[45];
                            if (mptkEvent.Value == 86)
                                sphere.transform.position = vertices[54];
                            if (mptkEvent.Value == 98)
                                sphere.transform.position = vertices[62];
                            break;
                        case 3:
                            if (mptkEvent.Value == 39)
                                sphere.transform.position = vertices[3];
                            if (mptkEvent.Value == 51)
                                sphere.transform.position = vertices[23];
                            if (mptkEvent.Value == 63)
                                sphere.transform.position = vertices[32];
                            if (mptkEvent.Value == 75)
                                sphere.transform.position = vertices[40];
                            if (mptkEvent.Value == 87)
                                sphere.transform.position = vertices[49];
                            if (mptkEvent.Value == 99)
                                sphere.transform.position = vertices[69];
                            break;
                        case 4:
                            if (mptkEvent.Value == 40)
                                sphere.transform.position = vertices[10];
                            if (mptkEvent.Value == 52)
                                sphere.transform.position = vertices[18];
                            if (mptkEvent.Value == 64)
                                sphere.transform.position = vertices[27];
                            if (mptkEvent.Value == 76)
                                sphere.transform.position = vertices[47];
                            if (mptkEvent.Value == 88)
                                sphere.transform.position = vertices[56];
                            if (mptkEvent.Value == 100)
                                sphere.transform.position = vertices[64];
                            break;
                        case 5:
                            if (mptkEvent.Value == 41)
                                sphere.transform.position = vertices[5];
                            if (mptkEvent.Value == 53)
                                sphere.transform.position = vertices[13];
                            if (mptkEvent.Value == 65)
                                sphere.transform.position = vertices[34];
                            if (mptkEvent.Value == 77)
                                sphere.transform.position = vertices[42];
                            if (mptkEvent.Value == 89)
                                sphere.transform.position = vertices[51];
                            if (mptkEvent.Value == 101)
                                sphere.transform.position = vertices[71];
                            break;
                        case 6:
                            if (mptkEvent.Value == 42)
                                sphere.transform.position = vertices[20];
                            if (mptkEvent.Value == 54)
                                sphere.transform.position = vertices[29];
                            if (mptkEvent.Value == 66)
                                sphere.transform.position = vertices[37];
                            if (mptkEvent.Value == 78)
                                sphere.transform.position = vertices[58];
                            if (mptkEvent.Value == 90)
                                sphere.transform.position = vertices[66];
                            break;
                        case 7:
                            if (mptkEvent.Value == 43)
                                sphere.transform.position = vertices[7];
                            if (mptkEvent.Value == 55)
                                sphere.transform.position = vertices[15];
                            if (mptkEvent.Value == 67)
                                sphere.transform.position = vertices[44];
                            if (mptkEvent.Value == 79)
                                sphere.transform.position = vertices[53];
                            if (mptkEvent.Value == 91)
                                sphere.transform.position = vertices[61];
                            break;
                        case 8:
                            if (mptkEvent.Value == 44)
                                sphere.transform.position = vertices[2];
                            if (mptkEvent.Value == 56)
                                sphere.transform.position = vertices[22];
                            if (mptkEvent.Value == 68)
                                sphere.transform.position = vertices[31];
                            if (mptkEvent.Value == 80)
                                sphere.transform.position = vertices[39];
                            if (mptkEvent.Value == 92)
                                sphere.transform.position = vertices[68];
                            break;
                        case 9:
                            if (mptkEvent.Value == 45)
                                sphere.transform.position = vertices[9];
                            if (mptkEvent.Value == 57)
                                sphere.transform.position = vertices[17];
                            if (mptkEvent.Value == 69)
                                sphere.transform.position = vertices[26];
                            if (mptkEvent.Value == 81)
                                sphere.transform.position = vertices[46];
                            if (mptkEvent.Value == 93)
                                sphere.transform.position = vertices[55];
                            if (mptkEvent.Value == 105)
                                sphere.transform.position = vertices[63];
                            break;
                        case 10:
                            if (mptkEvent.Value == 46)
                                sphere.transform.position = vertices[4];
                            if (mptkEvent.Value == 58)
                                sphere.transform.position = vertices[12];
                            if (mptkEvent.Value == 70)
                                sphere.transform.position = vertices[33];
                            if (mptkEvent.Value == 82)
                                sphere.transform.position = vertices[41];
                            if (mptkEvent.Value == 94)
                                sphere.transform.position = vertices[50];
                            if (mptkEvent.Value == 106)
                                sphere.transform.position = vertices[70];
                            break;
                        case 11:
                            if (mptkEvent.Value == 47)
                                sphere.transform.position = vertices[11];
                            if (mptkEvent.Value == 59)
                                sphere.transform.position = vertices[19];
                            if (mptkEvent.Value == 71)
                                sphere.transform.position = vertices[28];
                            if (mptkEvent.Value == 83)
                                sphere.transform.position = vertices[36];
                            if (mptkEvent.Value == 95)
                                sphere.transform.position = vertices[57];
                            if (mptkEvent.Value == 107)
                                sphere.transform.position = vertices[65];
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