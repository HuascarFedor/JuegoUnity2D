using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    Rigidbody2D rb2d;
    Vector2 movementInput;
    Animator animator;

    private int currentLives;
    public int maxLives = 100;

    private bool gameIsPaused = false;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentLives = maxLives;
        UIManager.Instance.UpdateLives(currentLives);
    }

    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        movementInput = movementInput.normalized;

        animator.SetFloat("Horizontal", Math.Abs(movementInput.x));
        animator.SetFloat("Vertical", Math.Abs(movementInput.y));

        CheckFlip();

        OpenCloseInventory();
        OpenClosePauseMenu();
    }

    private void FixedUpdate()
    {
        rb2d.linearVelocity = movementInput * speed;
    }

    void CheckFlip()
    {
        if ((movementInput.x > 0 && transform.localScale.x < 0) ||
        (movementInput.x < 0 && transform.localScale.x > 0))
        {
            transform.localScale = new Vector3(
                transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    void OpenCloseInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UIManager.Instance.OpenOrCloseInventory();
        }
    }

    void OpenClosePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameIsPaused)
            {
                UIManager.Instance.ResumeGame();
                gameIsPaused = false;
            }
            else
            {
                UIManager.Instance.PauseGame();
                gameIsPaused = true;
            }
        }
    }
}
