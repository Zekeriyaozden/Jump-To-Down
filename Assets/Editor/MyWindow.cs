using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyWindow : EditorWindow
{


    public List<Material> shaderMaterials;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/My Window")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        MyWindow window = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow));
        window.Show();
    }

    private void resetMaterial()
    {
        foreach (var shad in shaderMaterials)
        {
            shad.SetFloat("_Ring", 1f);
        }
    }    
    private void resetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    void OnGUI()
    {


   
        /*if (GUILayout.Button("Reser Shader Material"))
        {
            try
            {
                resetMaterial();
                Debug.Log("Material was Reseted!!!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }*/
        //---------------------------------
        if (GUILayout.Button("Reser PlayerPrefs Data"))
        {
            try
            {
                resetPlayerPrefs();
                Debug.Log("PlayerPrefs Data was Reseted!!!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        
    }
}
