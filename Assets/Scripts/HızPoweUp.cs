using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public float speedBoost = 5f; // Ekstra hız miktarı
    public float duration = 5f;  // Hız etkisinin süresi

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Eğer Power-Up'a çarpan nesne "Player" tag'ine sahipse
        if (collision.CompareTag("Player"))
        {
            // Oyuncunun PlayerMovement scriptine eriş
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                // Oyuncuya hız artırma etkisini uygula
                playerMovement.ActivateSpeedBoost(speedBoost, duration);
            }

            // Power-Up nesnesini sahneden kaldır
            Destroy(gameObject);
        }
    }
}