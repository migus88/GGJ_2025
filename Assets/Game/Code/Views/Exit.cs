using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour, IExit
{
    public Vector3 Position => gameObject.transform.position;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Fix this shit
        SceneManager.LoadScene("WinScene");
    }
}
