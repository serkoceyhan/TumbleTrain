using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ShoppingCart : MonoBehaviour
{
    public TextMeshProUGUI itemCountText;
    public GameObject losePanel; // Kaybetme ekranı

    private List<GameObject> itemsInCart = new List<GameObject>();

    private void Start()
    {
        if (itemCountText == null)
        {
            itemCountText = GameObject.Find("ItemCountText")?.GetComponent<TextMeshProUGUI>();

            if (itemCountText == null)
            {
                Debug.LogError("HATA: itemCountText sahnede bulunamadı! Lütfen Inspector'dan atayın.");
            }
        }

        // Lose ekranını başlangıçta kapat
        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }

        UpdateItemCount();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallingObject"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("HATA: " + collision.gameObject.name + " nesnesinde Rigidbody2D yok!");
                return;
            }

            if (!itemsInCart.Contains(collision.gameObject))
            {
                itemsInCart.Add(collision.gameObject);
                Debug.Log("Nesne sepete eklendi: " + collision.gameObject.name);
                UpdateItemCount();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FallingObject"))
        {
            Debug.Log("Nesne sepete girdi ama hemen çıktı: " + collision.gameObject.name);
            StartCoroutine(RemoveAfterDelay(collision.gameObject));
        }
    }

    private IEnumerator RemoveAfterDelay(GameObject item)
    {
        yield return new WaitForSeconds(0.2f);

        if (itemsInCart.Contains(item))
        {
            itemsInCart.Remove(item);
            Debug.Log("Nesne gecikmeli çıkarıldı: " + item.name);
            UpdateItemCount();
        }
    }

    private void UpdateItemCount()
    {
        if (itemCountText == null)
        {
            Debug.LogError("HATA: itemCountText atanmamış!");
            return;
        }

        itemCountText.text = "Sepetteki Nesneler: " + itemsInCart.Count;
        Debug.Log("Sepetteki nesne sayısı güncellendi: " + itemsInCart.Count);

        // Ürün sayısı 0 olursa oyunu bitir
        if (itemsInCart.Count == 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Oyun Bitti! Lose ekranı açılıyor...");

        // Lose panelini aç
        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
    }

    // Oyunu yeniden başlatan fonksiyon
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
