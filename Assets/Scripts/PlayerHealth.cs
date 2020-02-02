using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject PlayerContainer;
    public float countdown;
    bool startedCountdown;
    public int PlayerLives;
    // Start is called before the first frame update
    void Start()
    {
        countdown = 1.5f;
        PlayerLives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown <= 0)
        {
            //die
            if(PlayerLives >= 1)
            {
                PlayerLives--;
                gameObject.GetComponent<DamageHandler>().damage = 0;
                gameObject.GetComponentInChildren<Rigidbody2D>().velocity = Vector2.zero; 
                PlayerContainer.transform.position = new Vector2(0f, 5f);
                countdown = 1.5f; 
            }
            else
            {
                SceneManager.LoadScene("StartMenu");
            }
        }

        if (startedCountdown)
        {
            countdown -= Time.deltaTime;
        }
    }

    private void OnBecameInvisible()
    {
        startedCountdown = true; 
    }

    private void OnBecameVisible()
    {
        startedCountdown = false;
        countdown = 1.5f;
    }
}
