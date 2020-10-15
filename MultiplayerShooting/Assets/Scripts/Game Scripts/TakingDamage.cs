using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Image healthBar;
    public float startHealth = 100;
    private float health;
    void Start()
    {
        health = startHealth;
        healthBar.fillAmount = health / startHealth;
    } 

    [PunRPC]
    public void TakeDamage(float _damage)
    {
        health -= _damage;
        Debug.Log(health);
        healthBar.fillAmount = health / startHealth;

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if (photonView.IsMine)
        {
            PixelGunGameManager.instance.LeaveRoom();
        }
    }
}
