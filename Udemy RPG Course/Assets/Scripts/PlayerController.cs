using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D playerRB = null;
    Animator playerAnim = null;
    public bool canMove { get; set; }
    public string lastScene { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        canMove = true;
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canMove)
        {
            playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        }
        else
        {
            playerRB.velocity = Vector2.zero;
        }
        playerAnim.SetFloat("moveX", playerRB.velocity.x);
        playerAnim.SetFloat("moveY", playerRB.velocity.y);
        if (playerRB.velocity.x != 0 || playerRB.velocity.y != 0)
        {
            playerAnim.SetFloat("lastMoveX", playerRB.velocity.x);
            playerAnim.SetFloat("lastMoveY", playerRB.velocity.y);
        }
    }
}
