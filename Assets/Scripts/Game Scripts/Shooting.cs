﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Shooting : MonoBehaviour
{
    [SerializeField]
    Camera fpsCamera;

    public float fireRate = 0.1f;
    float fireTimer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }
        RaycastHit _hit;
        if (Input.GetButton("Fire1") && fireTimer>fireRate)
        {
            fireTimer = 0.0f;
            Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out _hit, 100))
            {
                Debug.Log(_hit.collider.gameObject.name);
                if (_hit.collider.gameObject.CompareTag("Player") && !_hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    _hit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10f);
                }
            }
        }
    }
}
