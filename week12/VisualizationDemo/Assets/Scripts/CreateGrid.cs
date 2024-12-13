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
            
            GameObject vertex1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            vertex1.transform.localScale -= new Vector3(0.7f, 0.7f, 0.7f);
            var vertex1Renderer = vertex1.GetComponent<Renderer>();
            vertex1Renderer.material.SetColor("_Color", Color.white); 
            vertex1.transform.position = vertices[i];
            
            GameObject vertex2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            vertex2.transform.localScale -= new Vector3(0.7f, 0.7f, 0.7f);
            var vertex2Renderer = vertex2.GetComponent<Renderer>();
            vertex2Renderer.material.SetColor("_Color", Color.white); 
            vertex2.transform.position = vertices[i + 12];
            
            if (i == 0)
                DestroyImmediate(vertex1);
        }

        for (int i = 24, x = 0; i < 36; i++, x++)
        {
            vertices[i] = new Vector3(x, 2);
            vertices[i + 12] = new Vector3(x + 0.5f, 3);
            
            GameObject vertex3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            vertex3.transform.localScale -= new Vector3(0.7f, 0.7f, 0.7f);
            var vertex3Renderer = vertex3.GetComponent<Renderer>();
            vertex3Renderer.material.SetColor("_Color", Color.white); 
            vertex3.transform.position = vertices[i];
            
            GameObject vertex4 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            vertex4.transform.localScale -= new Vector3(0.7f, 0.7f, 0.7f);
            var vertex4Renderer = vertex4.GetComponent<Renderer>();
            vertex4Renderer.material.SetColor("_Color", Color.white); 
            vertex4.transform.position = vertices[i + 12];
            
            if (i == 24)
                DestroyImmediate(vertex3);
        }

        for (int i = 48, x = 0; i < 60; i++, x++)
        {
            vertices[i] = new Vector3(x, 4);
            vertices[i + 12] = new Vector3(x + 0.5f, 5);
            
            GameObject vertex5 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            vertex5.transform.localScale -= new Vector3(0.7f, 0.7f, 0.7f);
            var vertex5Renderer = vertex5.GetComponent<Renderer>();
            vertex5Renderer.material.SetColor("_Color", Color.white); 
            vertex5.transform.position = vertices[i];
            
            GameObject vertex6 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            vertex6.transform.localScale -= new Vector3(0.7f, 0.7f, 0.7f);
            var vertex6Renderer = vertex6.GetComponent<Renderer>();
            vertex6Renderer.material.SetColor("_Color", Color.white); 
            vertex6.transform.position = vertices[i + 12];
            
            if (i == 48)
                DestroyImmediate(vertex5);
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
                            else if (mptkEvent.Value == 48)
                                sphere.transform.position = vertices[14];
                            else if (mptkEvent.Value == 60)
                                sphere.transform.position = vertices[35];
                            else if (mptkEvent.Value == 72)
                                sphere.transform.position = vertices[43];
                            else if (mptkEvent.Value == 84)
                                sphere.transform.position = vertices[52];
                            else if (mptkEvent.Value == 96)
                                sphere.transform.position = vertices[60];
                            else
                                sphere.transform.position = vertices[6];
                            break;
                        case 1:
                            if (mptkEvent.Value == 37)
                                sphere.transform.position = vertices[1];
                            else if (mptkEvent.Value == 49)
                                sphere.transform.position = vertices[21];
                            else if (mptkEvent.Value == 61)
                                sphere.transform.position = vertices[30];
                            else if (mptkEvent.Value == 73)
                                sphere.transform.position = vertices[38];
                            else if (mptkEvent.Value == 85)
                                sphere.transform.position = vertices[59];
                            else if (mptkEvent.Value == 97)
                                sphere.transform.position = vertices[67];
                            else
                                sphere.transform.position = vertices[1];
                            break;
                        case 2:
                            if (mptkEvent.Value == 38)
                                sphere.transform.position = vertices[8];
                            else if (mptkEvent.Value == 50)
                                sphere.transform.position = vertices[16];
                            else if (mptkEvent.Value == 62)
                                sphere.transform.position = vertices[25];
                            else if (mptkEvent.Value == 74)
                                sphere.transform.position = vertices[45];
                            else if (mptkEvent.Value == 86)
                                sphere.transform.position = vertices[54];
                            else if (mptkEvent.Value == 98)
                                sphere.transform.position = vertices[62];
                            else
                                sphere.transform.position = vertices[8];
                            break;
                        case 3:
                            if (mptkEvent.Value == 39)
                                sphere.transform.position = vertices[3];
                            else if (mptkEvent.Value == 51)
                                sphere.transform.position = vertices[23];
                            else if (mptkEvent.Value == 63)
                                sphere.transform.position = vertices[32];
                            else if (mptkEvent.Value == 75)
                                sphere.transform.position = vertices[40];
                            else if (mptkEvent.Value == 87)
                                sphere.transform.position = vertices[49];
                            else if (mptkEvent.Value == 99)
                                sphere.transform.position = vertices[69];
                            else
                                sphere.transform.position = vertices[3];
                            break;
                        case 4:
                            if (mptkEvent.Value == 40)
                                sphere.transform.position = vertices[10];
                            else if (mptkEvent.Value == 52)
                                sphere.transform.position = vertices[18];
                            else if (mptkEvent.Value == 64)
                                sphere.transform.position = vertices[27];
                            else if (mptkEvent.Value == 76)
                                sphere.transform.position = vertices[47];
                            else if (mptkEvent.Value == 88)
                                sphere.transform.position = vertices[56];
                            else if (mptkEvent.Value == 100)
                                sphere.transform.position = vertices[64];
                            else
                                sphere.transform.position = vertices[10];
                            break;
                        case 5:
                            if (mptkEvent.Value == 41)
                                sphere.transform.position = vertices[5];
                            else if (mptkEvent.Value == 53)
                                sphere.transform.position = vertices[13];
                            else if (mptkEvent.Value == 65)
                                sphere.transform.position = vertices[34];
                            else if (mptkEvent.Value == 77)
                                sphere.transform.position = vertices[42];
                            else if (mptkEvent.Value == 89)
                                sphere.transform.position = vertices[51];
                            else if (mptkEvent.Value == 101)
                                sphere.transform.position = vertices[71];
                            else
                                sphere.transform.position = vertices[5];
                            break;
                        case 6:
                            if (mptkEvent.Value == 42)
                                sphere.transform.position = vertices[20];
                            else if (mptkEvent.Value == 54)
                                sphere.transform.position = vertices[29];
                            else if (mptkEvent.Value == 66)
                                sphere.transform.position = vertices[37];
                            else if (mptkEvent.Value == 78)
                                sphere.transform.position = vertices[58];
                            else if (mptkEvent.Value == 90)
                                sphere.transform.position = vertices[66];
                            else
                                sphere.transform.position = vertices[20];
                            break;
                        case 7:
                            if (mptkEvent.Value == 43)
                                sphere.transform.position = vertices[7];
                            else if (mptkEvent.Value == 55)
                                sphere.transform.position = vertices[15];
                            else if (mptkEvent.Value == 67)
                                sphere.transform.position = vertices[44];
                            else if (mptkEvent.Value == 79)
                                sphere.transform.position = vertices[53];
                            else if (mptkEvent.Value == 91)
                                sphere.transform.position = vertices[61];
                            else
                                sphere.transform.position = vertices[7];
                            break;
                        case 8:
                            if (mptkEvent.Value == 44)
                                sphere.transform.position = vertices[2];
                            else if (mptkEvent.Value == 56)
                                sphere.transform.position = vertices[22];
                            else if (mptkEvent.Value == 68)
                                sphere.transform.position = vertices[31];
                            else if (mptkEvent.Value == 80)
                                sphere.transform.position = vertices[39];
                            else if (mptkEvent.Value == 92)
                                sphere.transform.position = vertices[68];
                            else
                                sphere.transform.position = vertices[2];
                            break;
                        case 9:
                            if (mptkEvent.Value == 45)
                                sphere.transform.position = vertices[9];
                            else if (mptkEvent.Value == 57)
                                sphere.transform.position = vertices[17];
                            else if (mptkEvent.Value == 69)
                                sphere.transform.position = vertices[26];
                            else if (mptkEvent.Value == 81)
                                sphere.transform.position = vertices[46];
                            else if (mptkEvent.Value == 93)
                                sphere.transform.position = vertices[55];
                            else if (mptkEvent.Value == 105)
                                sphere.transform.position = vertices[63];
                            else
                                sphere.transform.position = vertices[9];
                            break;
                        case 10:
                            if (mptkEvent.Value == 46)
                                sphere.transform.position = vertices[4];
                            else if (mptkEvent.Value == 58)
                                sphere.transform.position = vertices[12];
                            else if (mptkEvent.Value == 70)
                                sphere.transform.position = vertices[33];
                            else if (mptkEvent.Value == 82)
                                sphere.transform.position = vertices[41];
                            else if (mptkEvent.Value == 94)
                                sphere.transform.position = vertices[50];
                            else if (mptkEvent.Value == 106)
                                sphere.transform.position = vertices[70];
                            else
                                sphere.transform.position = vertices[4];
                            break;
                        case 11:
                            if (mptkEvent.Value == 47)
                                sphere.transform.position = vertices[11];
                            else if (mptkEvent.Value == 59)
                                sphere.transform.position = vertices[19];
                            else if (mptkEvent.Value == 71)
                                sphere.transform.position = vertices[28];
                            else if (mptkEvent.Value == 83)
                                sphere.transform.position = vertices[36];
                            else if (mptkEvent.Value == 95)
                                sphere.transform.position = vertices[57];
                            else if (mptkEvent.Value == 107)
                                sphere.transform.position = vertices[65];
                            else
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