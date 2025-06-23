using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour
{
    public void LoadMarkerlessScene()
    {
        SceneManager.LoadScene("MarkerlessScene");
    }

    public void LoadMarkerBasedScene()
    {
        SceneManager.LoadScene("MarkerBasedScene");
    }
}
