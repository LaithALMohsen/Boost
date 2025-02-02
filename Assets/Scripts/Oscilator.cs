using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    Vector3 startingPosition;
  [SerializeField]  Vector3 movementVector;

    float movementFactor;
    [SerializeField] float period = 2f;
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if(period == 0) { return;  }


        float cycles = Time.time / period;




        const float tau = Mathf.PI * 2;
        float rawSineWave = Mathf.Sin(cycles * tau);


        movementFactor = (rawSineWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
