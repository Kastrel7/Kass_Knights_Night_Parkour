using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClicked : MonoBehaviour
{
    public void Click()
    {
        SceneManager.LoadScene("Main");
    }
}
