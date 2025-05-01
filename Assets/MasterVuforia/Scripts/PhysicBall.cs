using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicBall : MonoBehaviour
{
    // Ejecutar la phisica solo si se apunta al target
    private Rigidbody _rigibody;

    // Start is called before the first frame update
    void Start()
    {
        _rigibody = GetComponent<Rigidbody>();
    }

    public void EnableRigibody(bool enable)
    {
        // Si enable es true, se habilitan las restricciones del rigidbody
        _rigibody.constraints = enable ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
    }

}
