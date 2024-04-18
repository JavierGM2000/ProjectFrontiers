using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    [SerializeField]
    GameObject currentCanvas;

    public void changeCanvas(GameObject newCanvas)
    {
        currentCanvas.SetActive(false);
        currentCanvas = newCanvas;
        currentCanvas.SetActive(true);
    }

    // Update is called once per frame
    public void changeLevel(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
    }
}
