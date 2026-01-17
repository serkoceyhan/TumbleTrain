using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için gerekli

public class LevelChanger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NextLevel")) // Eğer "NextLevel" tag'lı nesneye temas ederse
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Mevcut sahne numarası
        SceneManager.LoadScene(2); // Bir sonraki sahneye geç
    }
}