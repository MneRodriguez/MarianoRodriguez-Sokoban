using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JuegoManager : MonoBehaviour
{
    public ConstructorNvls m_ConstructorNvls;
    public GameObject m_NextButton;  
    private bool m_ReadyForInput;
    public ControlJgdr m_Player;

    private void Start()
    {
        m_ConstructorNvls.Build();
        m_Player = FindObjectOfType<ControlJgdr>();

        
        ResetScene();
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();

        if (moveInput.sqrMagnitude > 0.5) // Cuando se presiona una tecla
        {
            if (m_ReadyForInput)
            {
                m_ReadyForInput = false;
                m_Player.Move(moveInput);
                //m_NextButton.SetActive(IsLevelComplete());
            }
        }

        else { m_ReadyForInput = true; }
    }

    public void NextLevel()
    {
        m_NextButton.SetActive(true);
        m_ConstructorNvls.SiguienteNivel();
        m_ConstructorNvls.Build();
        StartCoroutine(ResetSceneAsync());
    }

    public void ResetScene()
    {
        StartCoroutine(ResetSceneAsync());
    }

    public bool SiElNivelSeCompleto()
    {
        ScriptCaja[] boxes = FindObjectsOfType<ScriptCaja>();

        foreach (var box in boxes)
        {
            if (!box.m_SobreCruz) return false;
        }
        return true;
    }

    IEnumerator ResetSceneAsync()
    {
        if (SceneManager.sceneCount > 1)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("EscenaDelNivel");

            while (!asyncUnload.isDone)
            {
                yield return null;
                Debug.Log("Cerrando...");
            }
            Debug.Log("Hecho");
            Resources.UnloadUnusedAssets();
        }
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("EscenaDelNivel", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
            Debug.Log("Cargando...");
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("EscenaDelNivel"));
        m_ConstructorNvls.Build();
        m_Player = FindObjectOfType<ControlJgdr>();
        Debug.Log("Nivel cargado");

    }
}
