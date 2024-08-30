using System.Collections;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEditor;

public class Parse_inputfile : MonoBehaviour
{
    /*
    public static string[,] atoms;
    //public static string[][,] frames;
    public static List<string[,]> frames = new List<string[,]>();
    public static List<List<int>> connectivities = new List<List<int>>();
    public static List<List<List<int>>> frame_connectivities = new List<List<List<int>>>();
    public static int num_atoms;
    //public static SerializedObject tagManager;
    //public static SerializedProperty tagsProp;
    public static Vector3 centroid;
    public static string file_path = "";

    // Start is called before the first frame update
    void Start()
    {
        //tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        //tagsProp = tagManager.FindProperty("tags");
        print("testing if the object is disabled");
        
        // read_file();
        // tagManager.ApplyModifiedProperties();
        // tagManager.Update();
        // print("List of tags" + UnityEditorInternal.InternalEditorUtility.tags.Length);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void compute_centroid()
    {
        float x = 0;
        float y = 0;
        float z = 0;
        for (int i = 0; i<num_atoms; i++)
        {
            x += float.Parse(frames[0][i, 2]);
            y += float.Parse(frames[0][i, 3]);
            z += float.Parse(frames[0][i, 4]);
        }
        x /= num_atoms;
        y /= num_atoms;
        z /= num_atoms;
        centroid = new Vector3(x, y, z);
    }

    void create_tag(string frame_number)
    {
        // tagsProp.InsertArrayElementAtIndex(0);
        // SerializedProperty n = tagsProp.GetArrayElementAtIndex(0);
        // n.stringValue = frame_number;

        // print("please dont enter");
    }

    public void read_file()
    {
        //string[] lines = File.ReadAllLines(@"Assets/test_files/helix-alpha.xyz");
        //string[] lines = File.ReadAllLines(@"Assets/test_files/benzene.xyz");
        //string[] lines = File.ReadAllLines(@"Assets/test_files/peptide.arc");
        string[] lines = File.ReadAllLines(@file_path);
        int i = -1;
        int counter = 0;
        int frame_num = 0;

        foreach (string line in lines)
        {
            //ignore first line
            if (i != -1)
            {
                if (counter!=num_atoms)
                {
                    string[] s = Regex.Split(line, @"\s+");
                    //s[0] appears to be blank? s[1] represents atom #
                    atoms[i, 0] = s[1];
                    atoms[i, 1] = s[2];
                    atoms[i, 2] = s[3];
                    atoms[i, 3] = s[4];
                    atoms[i, 4] = s[5];
                    atoms[i, 5] = s[6];

                    List<int> l = new List<int>();

                    for (int j = 7; j < s.Length; j++)
                    {
                        l.Add(int.Parse(s[j]));
                    }

                    connectivities.Add(l);
                    counter++;
                }
                else
                {
                    counter = 0;
                    //print(atoms.Length);
                    frames.Add(atoms);
                    frame_connectivities.Add(connectivities);
                    atoms = new string[num_atoms, 6];
                    connectivities = new List<List<int>>();
                    create_tag(frame_num.ToString());
                    frame_num++;
                    i = -1;
                }
                
            }
            else
            {
                string[] s = Regex.Split(line, @"\s+");
                num_atoms = int.Parse(s[1]);
                atoms = new string[num_atoms, 6];
                connectivities = new List<List<int>>();
                //print(num_atoms);
            }
            i++;

        }
        //Add the atoms and tag for the last frame
        frames.Add(atoms);
        frame_connectivities.Add(connectivities);
        create_tag(frame_num.ToString());

        //compute the centroid
        compute_centroid();

        //update tag manager
        //tagManager.ApplyModifiedProperties();
        //tagManager.Update();

    }*/
}
