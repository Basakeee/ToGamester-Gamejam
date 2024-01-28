using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public LayerMask playerLayer;
    public Vector2 Size;
    public Vector2 offset;
    public bool isBosskilled = false;
    private Vector3 off => offset;
    

    private void Update()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position + off, Size, 0f, playerLayer);
        if (hit != null)
        {
            if (gameObject.name == "Door" && isBosskilled) // Boss Door
            {
                Debug.Log("Boss Door");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if(gameObject.name != "Door") // Normal Door
            {
                Debug.Log("Normal DOor");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position + off, Size);
    }
}
