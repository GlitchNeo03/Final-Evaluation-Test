using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameManager _gameManager;
    Transform PlayerTransform;

    private void Start()
    {
        PlayerTransform = GetComponent<Transform>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bad"))
        {
            _gameManager.HealthSlider.value = _gameManager.HealthSlider.value - 3;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Flash"))
        {
            _gameManager.OnFlashCollect();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Health"))
        {
            _gameManager.HealthSlider.value = _gameManager.HealthSlider.maxValue;
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if(PlayerTransform.position.y <= -5)
        {
            _gameManager.OnDeath();
        }
    }

    public void OnEnd()
    {
        ThirdPersonController TPC = GetComponent<ThirdPersonController>();
        TPC.enabled = false;
    }
}
