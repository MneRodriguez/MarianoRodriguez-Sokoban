using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                m_Player.Moverse(moveInput);
                //m_NextButton.SetActive(IsLevelComplete());
            }
        }

        else { m_ReadyForInput = true; }
    }
}
