using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoomInScript : MonoBehaviour
{
    [SerializeField] float normalFOV = 57f;
    [SerializeField] float zoomFOV = 25f;

    CinemachineFreeLook vcam;
    CinemachineComposer comp;
    CinemachineComposer comp1;
    CinemachineComposer comp2;

    void Awake() {
        vcam = GetComponent<CinemachineFreeLook>();

    }

    void Update() {
        float FOV = normalFOV;
        comp = vcam.GetRig(0).GetCinemachineComponent<CinemachineComposer>();
        comp1 = vcam.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
        comp2 = vcam.GetRig(2).GetCinemachineComponent<CinemachineComposer>();
        if (Input.GetKey(KeyCode.Mouse1)){
            FOV = zoomFOV; 
            comp.m_ScreenX = 0.33f;
            comp1.m_ScreenX = 0.33f;
            comp2.m_ScreenX = 0.33f;

            comp.m_ScreenY = 0.83f;
            comp1.m_ScreenY = 0.83f;
            comp2.m_ScreenY = 0.83f;
        }
        vcam.m_Lens.FieldOfView = vcam.m_Lens.FieldOfView + (FOV - vcam.m_Lens.FieldOfView) * 0.5f;
        if(Input.GetKeyUp(KeyCode.Mouse1)){
            comp.m_ScreenX = 0.5f;
            comp1.m_ScreenX = 0.5f;
            comp2.m_ScreenX = 0.5f;
            
            comp.m_ScreenY = 0.5f;
            comp1.m_ScreenY = 0.5f;
            comp2.m_ScreenY = 0.5f;
        }
    }
}


 