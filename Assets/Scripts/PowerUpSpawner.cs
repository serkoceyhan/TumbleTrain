using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab; // Power-Up nesnesinin prefab'ı
    public Vector2 spawnAreaMin;     // Spawn alanının minimum köşesi
    public Vector2 spawnAreaMax;     // Spawn alanının maksimum köşesi
    public float spawnInterval = 5f; // Power-Up'ın rastgele çıkış süresi (saniye)

    void Start()
    {
        // Belirli bir zaman aralığıyla Power-Up spawn et
        InvokeRepeating(nameof(SpawnPowerUp), 0f, spawnInterval);
    }

    private void SpawnPowerUp()
    {
        // Rastgele bir pozisyon oluştur
        float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);

        Vector2 spawnPosition = new Vector2(x, y);

        // Power-Up prefab'ını sahneye yerleştir
        Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
    }
}