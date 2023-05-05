using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FinishScreen : MonoBehaviour
{
    public void MenuPrincipal()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2 );
    }
}
