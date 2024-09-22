using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private Transform _parent;
    private Rigidbody _rigidbody;
    private Vector3 _startPosition;
    public bool IsDrop {  get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _parent = transform.parent;

        _rigidbody.isKinematic = true;
        IsDrop = false;
        _startPosition = transform.localPosition;
    }

    public void Dropping()
    {
        _rigidbody.isKinematic = false;
        IsDrop = true;
        transform.SetParent(null);
    }

    public void CollectFruit()
    {

    }

    public void BackToTree()
    {
        _rigidbody.isKinematic = true;
        IsDrop = false;
        transform.SetParent(_parent);
        transform.localPosition = _startPosition;
    }
}
