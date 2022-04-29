using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRumble : MonoBehaviour
{
    public Vector3 initialPosition;
    public float shakeAmount = 0.2f;
    public float rumble = 0;
    public float decreaseAmount = 2f;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        if (rumble > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeAmount;
            rumble -= Time.deltaTime * decreaseAmount;
        }
        else
        {
            rumble = 0.0f;
            transform.localPosition = initialPosition;
        }
    }
}
