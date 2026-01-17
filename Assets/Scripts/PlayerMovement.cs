using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;          // Temel hız
    public float rotationSpeed = 100f; 
    public float jumpForce = 10f;    

    private Rigidbody2D rb;         
    private bool isGrounded = true;

    private float defaultSpeed;          // Orijinal hız
    private bool isSpeedBoostActive = false; // Aynı anda birden fazla speed boost etkisini engellemek için

    private bool isOnMud = false;        // Karakter şu anda toprak zeminde mi?
    private bool isOnIce = false;        // Karakter şu anda buz zeminde mi?

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultSpeed = speed; // Orijinal hızı kaydet
    }

    void Update()
    {
        HandleMovement();
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void HandleMovement()
    {
        // Eğer buz zemindeysek, tüm girişleri devre dışı bırak ve sürekli ileri hareket et
        if (isOnIce)
        {
            Debug.Log("Buz zemindesiniz, otomatik ileri hareket aktif.");
            Vector2 velocity = rb.velocity;
            velocity.x = speed; // Sürekli ileri hareket (sağa doğru)
            rb.velocity = velocity;
            return; // Diğer kontrolleri çalıştırma
        }

        // Hareket kontrolleri
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddTorque(-rotationSpeed * Time.deltaTime); // Dönüşü yavaşlat
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddTorque(rotationSpeed * Time.deltaTime); // Dönüşü hızlandır
        }

        Vector2 newVelocity = rb.velocity;

        // Hızlanma ve yavaşlama
        if (Input.GetKey(KeyCode.D))
        {
            newVelocity.x = speed; // Sağ tarafa hızlanma
        }
        else if (Input.GetKey(KeyCode.A))
        {
            newVelocity.x = -speed; // Sol tarafa hızlanma
        }
        else
        {
            newVelocity.x = 0; // D ve A tuşlarına basılmıyorsa dur
        }

        rb.velocity = newVelocity;
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Yukarı zıplama kuvveti
        isGrounded = false; // Karakter havada
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Zemin algılama
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
        }

        // Toprak zemine giriş
        if (collision.gameObject.CompareTag("Mud"))
        {
            isOnMud = true;
            speed = 3f; // Hızı yavaşlat
        }

        // Buz zemine giriş
        if (collision.gameObject.CompareTag("Ice"))
        {
            isOnIce = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Toprak zeminden çıkış
        if (collision.gameObject.CompareTag("Mud"))
        {
            isOnMud = false;
            speed = defaultSpeed; // Hızı eski haline getir
        }

        // Buz zeminden çıkış
        if (collision.gameObject.CompareTag("Ice"))
        {
            isOnIce = false;
        }
    }

    // Power-Up etkisi için fonksiyon
    public void ActivateSpeedBoost(float extraSpeed, float duration)
    {
        // Eğer toprak zemindeysek, speed boost uygulanmasın
        if (isOnMud)
        {
            return;
        }

        if (!isSpeedBoostActive) // Eğer bir hız artırma etkisi zaten aktif değilse
        {
            StartCoroutine(SpeedBoostCoroutine(extraSpeed, duration));
        }
    }

    // Coroutine: Hız artırma etkisini belirli bir süre uygular
    private IEnumerator SpeedBoostCoroutine(float extraSpeed, float duration)
    {
        isSpeedBoostActive = true;  // Etki aktif
        speed += extraSpeed;       // Hızı artır
        yield return new WaitForSeconds(duration); // Süreyi bekle
        speed = defaultSpeed;      // Hızı eski haline getir
        isSpeedBoostActive = false; // Etki bitti
    }
}
