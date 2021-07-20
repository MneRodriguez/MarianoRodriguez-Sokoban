using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level // Clase que define un único nivel
{
    public List<string> m_Rows = new List<string>();

    public int Altura { get { return m_Rows.Count; } } // Toma las columnas del txt
    public int Ancho // Toma las filas
    {
        get
        {
            int LongitudMax = 0;
            foreach (var r in m_Rows)
            {
                if (r.Length > LongitudMax) LongitudMax = r.Length;
            }
            
            return LongitudMax;
        }
    }
}

public class Niveles : MonoBehaviour
{
    public string NombreArchivo;
    public List<Level> m_Levels;

    private void Awake()
    {
        TextAsset textAsset = (TextAsset)Resources.Load(NombreArchivo);

        if (!textAsset)
        {
            Debug.Log("Niveles: " + NombreArchivo + ".txt no existe");
            return;
        }

        else { Debug.Log("Niveles cargado exitosamente"); }

        string completeText = textAsset.text;
        string[] lines;
        lines = completeText.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        m_Levels.Add(new Level());

        for (long i = 0; i < lines.LongLength; i++)
        {
            string line = lines[i];
            if (line.StartsWith(";"))
            {
                Debug.Log("Añadido nuevo nivel");
                m_Levels.Add(new Level());
                continue;
            }
            m_Levels[m_Levels.Count - 1].m_Rows.Add(line);
        }
    }
}
