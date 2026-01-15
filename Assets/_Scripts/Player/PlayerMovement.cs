using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    [Header("Movement Stats")]
    public float moveSpeed = 8f;
    public float jumpForce = 16f;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing;
    private bool canDash = true;

    [Header("Double Jump")]
    public int amountOfJumps = 2; // Cho phép nhảy 2 lần
    private int amountOfJumpsLeft;

    [Header("Components & Physics")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public TrailRenderer tr; // Kéo Component Trail Renderer vào đây để tạo vệt khi lướt

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isFacingRight = true;
    private bool isGrounded;

    // Animator
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // Nếu chưa có Animator thì code vẫn chạy ko lỗi
        amountOfJumpsLeft = amountOfJumps;
    }

    void Update()
    {
        if (isDashing) return; // Nếu đang lướt thì không nhận input khác

        // 1. Input Di chuyển
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // 2. Logic Nhảy & Double Jump
        CheckInput();

        // 3. Logic Dash (Phím Shift trái)
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        // 4. Quay mặt nhân vật
        Flip();
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        CheckSurroundings(); // Kiểm tra chạm đất

        // Di chuyển vật lý
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Update Animation (Sau này dùng)
        // anim.SetBool("IsRunning", horizontalInput != 0);
        // anim.SetBool("IsGrounded", isGrounded);
    }

    void CheckInput()
    {
        // Reset số lần nhảy khi chạm đất
        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (amountOfJumpsLeft > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                amountOfJumpsLeft--;
                // anim.SetTrigger("Jump");
            }
        }
    }

    void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // Coroutine xử lý lướt (Dash)
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0; // Tắt trọng lực để lướt thẳng
        rb.linearVelocity = new Vector2(transform.localScale.x * dashSpeed, 0f); // Lướt theo hướng mặt

        if (tr != null) tr.emitting = true; // Bật vệt sáng

        yield return new WaitForSeconds(dashDuration); // Chờ hết thời gian lướt

        if (tr != null) tr.emitting = false;
        rb.gravityScale = originalGravity; // Trả lại trọng lực
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown); // Chờ hồi chiêu
        canDash = true;
    }

    void Flip()
    {
        if (isFacingRight && horizontalInput < 0 || !isFacingRight && horizontalInput > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
