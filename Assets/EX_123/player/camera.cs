using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cam.transform.localRotation = cam.transform.localRotation * Quaternion.Euler(-mouseY, 0, 0);
        transform.localRotation = transform.localRotation * Quaternion.Euler(0, mouseX, 0);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
