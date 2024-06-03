using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCAm : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
