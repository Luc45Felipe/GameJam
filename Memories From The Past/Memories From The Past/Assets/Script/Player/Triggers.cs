using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Triggers : MonoBehaviour
{
    GameManager gameManager;
    Movement movement;

    [SerializeField] GameObject baseCamera;
    [SerializeField] TilemapRenderer fakeGround;

    private bool isColliding;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        movement = GetComponent<Movement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("FakeGround"))
        {
            fakeGround.enabled = false;
            baseCamera.SetActive(false);
        }

        if(other.gameObject.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            gameManager.EndStage();
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
            if(isColliding) return;
            isColliding = true;

            StartCoroutine(movement.GetDemage());

            StartCoroutine(Reset());
        }

        if(other.gameObject.CompareTag("Limbo"))
        {
            gameManager.Replay();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("FakeGround"))
        {
            fakeGround.enabled = true;
            baseCamera.SetActive(true);
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }
}
