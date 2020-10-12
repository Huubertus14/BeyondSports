using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Do all the needed tasks here before you open the next scene.
/// Like loading the data ...
/// </summary>
public class Preloader : MonoBehaviour
{
    [Header("Editor Referances:")]
    [SerializeField] private GameObject persistancePrefab;

    private void Awake()
    {
        Application.targetFrameRate = 25;
        //Spawn the persistance GO
        GameObject temp = Instantiate(persistancePrefab, Vector3.zero, Quaternion.identity);
        DontDestroyOnLoad(temp);

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(1);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //Check if the first few seconds are loaded
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
