using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class CylinderInteraction : MonoBehaviour
{
    public UnityEvent OnOpen;
    public UnityEvent OnClose;

    private Animator _animator;
    private bool _isOpen;
    // Optimización
    private int _hash_IsOpen = Animator.StringToHash("IsOpen");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _isOpen = !_isOpen;

        if (_isOpen)
        {
            OnOpen.Invoke();
        } else
        {
            OnClose.Invoke();
        }

        _animator.SetBool(_hash_IsOpen, _isOpen);
    }
}
