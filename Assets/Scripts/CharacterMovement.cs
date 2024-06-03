using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private Transform rayStart;
    [SerializeField] private GameObject crystalEffect;
    private Animator anim;
    private int rotateY = 0;
    private GameManager _gameManager;
    
    private bool _walkingRight = true;
    // Start is called before the first frame update
    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_gameManager.gameStarted)
        {
            return;
        }
        else
        {
            anim.SetTrigger("isStarted");
        }

        StartCoroutine(Wait(4));
        _rb.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;
    }
    
    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Switch();
        }
        

        RaycastHit hit;

        if (!Physics.Raycast(rayStart.position,-transform.up,out hit,Mathf.Infinity))
        {
            anim.SetTrigger("isFalling");
        }
        else
        {
            anim.SetTrigger("notFalling");
        }

        if (transform.position.y < -4)
        {
            _gameManager.EndGame();
        }
    }

    void Switch()
    {
        if (!_gameManager.gameStarted)
        {
            return;
        }
        _walkingRight = !_walkingRight;

        if (_walkingRight)
        {
            transform.rotation = Quaternion.Euler(0, rotateY+45, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, rotateY-45, 0);
        }
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag=="Points")
        {
            _gameManager.IncreaseScore();
            GameObject g = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(g,1);
            Destroy(other.gameObject);

        }
    }
}
