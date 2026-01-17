using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 startPosition; // Başlangıç pozisyonu
    public Vector3 endPosition;   // Hedef pozisyon
    public float speed = 2f;      // Platformun hareket hızı
    private bool movingToEnd = true; // Platformun hangi yöne hareket ettiğini belirler

    void Start()
    {
        // Platform başlangıç pozisyonuna yerleştiriliyor
        transform.position = startPosition;
    }

    void Update()
    {
        // Platformu hareket ettir
        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);

            // Hedef pozisyona ulaşıldıysa yön değiştir
            if (Vector3.Distance(transform.position, endPosition) < 0.1f)
            {
                movingToEnd = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);

            // Başlangıç pozisyonuna ulaşıldıysa yön değiştir
            if (Vector3.Distance(transform.position, startPosition) < 0.1f)
            {
                movingToEnd = true;
            }
        }
    }
}