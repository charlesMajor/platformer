using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int pointsForGem = 50;

    private AudioSource audio;
    [SerializeField] private AudioClip gemSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip victorySound;

    private bool playerIsDead = false;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Gem")
        {
            audio.PlayOneShot(gemSound);
            collision.gameObject.SetActive(false);
            GameManager.Instance.AddScore(pointsForGem);
        }

        if (collision.tag == "Monster" && !playerIsDead)
        {
            die();
        }

        if (collision.tag == "Exit")
        {
            exit();
        }
    }

    private void die()
    {
        playerIsDead = true;
        GetComponent<PlayerMovement>().blockMovement();
        GameManager.Instance.PlayerDie();
        audio.PlayOneShot(deathSound);
        StartCoroutine("ScaleDownPlayer");
        GameManager.Instance.RestartLevel(1);
    }

    private void exit()
    {
        GetComponent<PlayerMovement>().blockMovement();
        audio.clip = victorySound;
        audio.Play();
        GameManager.Instance.StartNextlevel(2);
    }

    IEnumerator ScaleDownPlayer()
    {
        for (float scale = 1; scale > 0; scale -= (1 * Time.deltaTime))
        {
            transform.localScale = new Vector3(
                transform.localScale.x * scale,
                transform.localScale.y * scale,
                transform.localScale.z * scale);
            yield return null;
        }
    }
}
