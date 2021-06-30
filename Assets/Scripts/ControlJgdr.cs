using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJgdr : MonoBehaviour
{
    private bool jgdrEnMovto = false;
    public Vector3 posOriginal, posTarget;

    public float velMovto = 0.4f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && !jgdrEnMovto)
        {
            StartCoroutine(Moverse(Vector3.up));
        }
        if (Input.GetKey(KeyCode.A) && !jgdrEnMovto)
        {
            StartCoroutine(Moverse(Vector3.left));
        }
        if (Input.GetKey(KeyCode.S) && !jgdrEnMovto)
        {
            StartCoroutine(Moverse(Vector3.down));
        }
        if (Input.GetKey(KeyCode.D) && !jgdrEnMovto)
        {
            StartCoroutine(Moverse(Vector3.right));
        }
    }

    private IEnumerator Moverse(Vector3 direction)
    {
        jgdrEnMovto = true;

        float elapsedTime = 0;

        posOriginal = transform.position;
        posTarget = posOriginal + direction;

        while(elapsedTime < velMovto)
        {
            transform.position = Vector3.Lerp(posOriginal, posTarget, (elapsedTime / velMovto));
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }

        transform.position = posTarget;

        jgdrEnMovto = false;
    }
}
