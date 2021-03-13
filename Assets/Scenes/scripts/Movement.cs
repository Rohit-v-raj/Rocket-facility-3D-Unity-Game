using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    [Range(0,1)]
    [SerializeField]
    float movementFactor;
    Vector3 startingpos;
    void Start()
    {
        startingpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2f;
        float rawsinwave = Mathf.Sin(cycles * tau);
        movementFactor = rawsinwave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = offset + startingpos;
    }
}
