using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject BadSphere;
    public Transform[] ShooterBoxes;
    public Slider HealthSlider;
    public Animator _PlayerAnimator;
    bool CanCheckHealth = true;
    public GameObject PauseMenu, IntroText, DeathScreen, WinScreen;
    bool isPaused = false;
    bool isDead = false;
    public PlayerManager _playerManager;
    public int FlashCount;
    public Text FlashCountText;

    private void Start()
    {
        Time.timeScale = 1;
        InvokeRepeating("Shoot", 0, 1);
        Destroy(IntroText, 5);
    }

    private void Shoot()
    {
        foreach(Transform t in ShooterBoxes)
        {
            GameObject clone = Instantiate(BadSphere, t);
            clone.transform.parent = t;
            Rigidbody rb = clone.GetComponent<Rigidbody>();
            rb.velocity = -Vector3.right * 25;
            Destroy(clone, 0.6f);
        }
    }

    private void Update()
    {
        
        if (CanCheckHealth && HealthSlider.value == 0)
        {
            OnDeath();
        }

        if (Input.GetKey(KeyCode.Escape) && !isPaused && !isDead)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
    }
    public void OnFlashCollect()
    {
        FlashCount++;
        FlashCountText.text = "Flash : " + FlashCount + "/5";
        if (FlashCount == 5) OnWin();
    }
    public void OnDeath()
    {
        _PlayerAnimator.SetTrigger("death");
        isDead = true;
        DeathScreen.SetActive(true);
        _playerManager.OnEnd();
        Time.timeScale = 0;
        CanCheckHealth = false;
    }

    public void OnWin()
    {
        _PlayerAnimator.SetBool("run", false);
        _PlayerAnimator.SetBool("crouch", false);
        _PlayerAnimator.SetBool("air", false);
        _PlayerAnimator.SetBool("sprint", false);
        _playerManager.OnEnd();
        WinScreen.SetActive(true);
    }
    public void Reload()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        isPaused = false;
        PauseMenu?.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
