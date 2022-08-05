using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorTools : Editor
{
    public int asda;
    [MenuItem("Tools/Reset Player Prefs")]
    public static void ResetPlayerPrefs()
    {
        try
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Player Prefs Reseted!!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    [MenuItem("Tools/Reset Shader Data")]
    public static void ResetShaderData()
    {
        
    }
}
