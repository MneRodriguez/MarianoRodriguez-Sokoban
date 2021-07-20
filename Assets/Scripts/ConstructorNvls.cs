using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelElement
{
    public string m_Character;
    public GameObject m_Prefab;
}

public class ConstructorNvls : MonoBehaviour
{
    public int m_NivelActual;
    public List<LevelElement> m_LevelElements;
    private Level m_Level;

    GameObject GetPrefab (char c)
    {
        LevelElement levelElement = m_LevelElements.Find(le => le.m_Character == c.ToString());
        if (levelElement != null)
        {
            return levelElement.m_Prefab;
        }
        else { return null; }
    }

    public void SiguienteNivel()
    {
        m_NivelActual++;
        if (m_NivelActual >= GetComponent<Niveles>().m_Levels.Count)
        {
            m_NivelActual = 0;
        }
    }

    public void Build()
    {
        m_Level = GetComponent<Niveles>().m_Levels[m_NivelActual];

        int startx = -m_Level.Ancho / 2;
        int x = startx;

        int y = -m_Level.Altura / 2;
        foreach (var row in m_Level.m_Rows)
        {
            foreach (var ch in row)
            {
                Debug.Log(ch);
                GameObject prefab = GetPrefab(ch);

                if (prefab)
                {
                    Debug.Log(prefab.name);
                    Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                x++;
            }
            y++;
            x = startx;
        }
    }
}
