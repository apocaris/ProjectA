using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{ // 카메라 흔들기

    public float ShakeAmount;

    //public Canvas canvas;

    float ShakeTime;
    Vector3 initialPosition;

    private void Start()

    {


        initialPosition = new Vector3(14f, 0f, -2.2f);

    }

    private void Update()

    {

        if (ShakeTime > 0)

        {

            transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;

            ShakeTime -= Time.deltaTime;

        }

        else

        {

            ShakeTime = 0.0f;

            transform.position = initialPosition;

            //canvas.renderMode = RenderMode.ScreenSpaceCamera;

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ShakeTime = 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            ShakeTime = 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ShakeTime = 0.1f;
        }

    }
}