using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{

    public Camera m_mainCamera;


	// Use this for initialization
	void Start ()
    {
        if (m_mainCamera == null)
            m_mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(transform.position + m_mainCamera.transform.rotation * Vector3.forward, m_mainCamera.transform.rotation * Vector3.up);

	}
}
