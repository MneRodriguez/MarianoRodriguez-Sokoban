using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCaja : MonoBehaviour
{
    public bool m_SobreCruz;

    public bool Move(Vector2 direction) // Inhabilita movto en diagonal
    {
        if (BoxBlocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction); // Permite mover caja al no estar bloqueada
            CambioEstadoCaja();
            return true;
        }
    }

    bool BoxBlocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;

        GameObject[] walls = GameObject.FindGameObjectsWithTag("Pared");

        foreach (var wall in walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                return true;
            }
        }

        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Caja");

        foreach (var box in boxes)
        {
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
            {
                return true;
            }
        }

        return false;
    }
    
    void CambioEstadoCaja()
    {
        GameObject[] cruces = GameObject.FindGameObjectsWithTag("TileConCruz");

        foreach (var cruz in cruces)
        {
            if (transform.position.x == cruz.transform.position.x && transform.position.y == cruz.transform.position.y)
            {
                GetComponent<SpriteRenderer>().color = Color.black; // SI LA CAJA ESTA SOBRE UNA CRUZ, SE COLOREA DE NEGRO
                m_SobreCruz = true;
                return;
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        m_SobreCruz = false;
    }

}
