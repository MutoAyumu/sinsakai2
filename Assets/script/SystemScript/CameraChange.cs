using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera m_mainCam;
    [SerializeField] CinemachineVirtualCamera m_secondCam;

    private void Start()
    {
        m_mainCam.MoveToTopOfPrioritySubqueue();
    }

    public void MainCam()
    {
        m_mainCam.MoveToTopOfPrioritySubqueue();
    }

    public void SecondCam()
    {
        m_secondCam.MoveToTopOfPrioritySubqueue();
    }
}
