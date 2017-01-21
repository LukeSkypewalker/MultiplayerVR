using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour
{ 
    public const int maxHealth = 100;
    [SyncVar (hook = "OnHealthChange")] public int health = maxHealth;
    public RectTransform healthBar;
    public bool destroyOnDeath;
    private NetworkStartPosition[] spawnPoints;

    void Start()
    {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();

        }
    }

    public void TakeDamage(int amount)
    {
        if (!isServer) return;

        health -= amount;
        if (health <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                health = maxHealth;
                RpcRespawn();
            }
            
        }
    }

    void OnHealthChange(int hlth)
    {
        healthBar.sizeDelta = new Vector2(hlth * 2, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            Vector3 spawnPoint = Vector3.zero;
            if(spawnPoints != null && spawnPoints.Length>0)
            {
                spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].transform.position;
            }
            transform.position = spawnPoint;
             
        }
    }
}
