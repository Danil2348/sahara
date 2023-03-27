using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitcamera : MonoBehaviour
{
    public float sensivity = 5f;
    private float titleAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis ("Mouse Y");
        titleAngle -= mouseY * sensivity;
        titleAngle = Mathf. Clamp(titleAngle, -20f,90f);
        transform.localRotation = Quaternion.Euler(titleAngle,0,0);
    }
}
