using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Text gameOverText;
    public Text finishText;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        gameOverText.enabled = false;
        finishText.enabled = false;

        player = FindObjectOfType<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
     if (player.isDead)
        {
            gameOverText.enabled = true;
        }

        if (player.levelFinished)
        {
            finishText.enabled = true;
        }
        if (player.isDead && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if ((player.isDead || player.levelFinished) && Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (player.levelFinished && Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
