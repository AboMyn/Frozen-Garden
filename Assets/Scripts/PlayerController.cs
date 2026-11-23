using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 20f;
    public float jumpForce = 7f;
    public float extraGravity = 2.5f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Animator anim;

    private bool isGrounded;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        rb.freezeRotation = true;
    }

    void Update()
    {
        // Проверка земли
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // ВАЖНО: передаём состояние земли в Animator
        anim.SetBool("isGrounded", isGrounded);

        // Движение
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (moveInput.magnitude > 0.1f)
        {
            Quaternion rotation = Quaternion.LookRotation(moveInput);
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        // Анимация ходьбы
        anim.SetBool("isWalk", moveInput.magnitude > 0.1f && isGrounded);

        // Прыжок
        if (Input.GetButtonDown("Jump") && isGrounded && rb.linearVelocity.y <= 0.1f)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Анимации прыжка/падения
        anim.SetBool("isJumping", !isGrounded && rb.linearVelocity.y > 0.1f);
        anim.SetBool("isFalling", !isGrounded && rb.linearVelocity.y < -0.1f);
    }

    void FixedUpdate()
    {
        // Движение по XZ, не трогаем Y
        rb.linearVelocity = new Vector3(moveInput.x * speed, rb.linearVelocity.y, moveInput.z * speed);

        // Дополнительная гравитация для более быстрого падения
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
/*using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 20f;
    private Rigidbody rb;
    private Animator anim;

    private bool isJumping;
    private bool isGrounded;

    public float jumpForce = 10f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        Debug.Log("Grounded: " + isGrounded);


        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (moveInput.magnitude > 0.1f)
        {
            Quaternion rotation = Quaternion.LookRotation(moveInput);
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        anim.SetBool("isWalk", moveInput.magnitude > 0.1f);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            anim.SetBool("isJumping", true);
        }

        if (!isGrounded && rb.linearVelocity.y < -0.1f)
        {
            anim.SetBool("isFalling", true);
        }
        else
        {
            anim.SetBool("isFalling", false);
        }

        if (isGrounded)
        {
            anim.SetBool("isJumping", false);
        }
        
        rb.linearVelocity = moveInput * speed;

        
    }
}*/

/*using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 20f;
    private Rigidbody rb;
    private Animator anim;

    private bool isJumping;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (moveInput.magnitude > 0.1f)
        {
            Quaternion rotation = Quaternion.LookRotation(moveInput);
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        anim.SetBool("isWalk", moveInput.magnitude > 0.1f);
        
        rb.linearVelocity = moveInput * speed;
    }
}*/