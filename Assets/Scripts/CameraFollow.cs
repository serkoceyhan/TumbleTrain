using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTarget; // Basketbol topu
    public float smoothSpeed = 0.125f; // Kameranın hareket hızı
    public Vector3 offset;             // Kamera ile top arasındaki mesafe

    private Transform currentTarget;   // Şu an takip edilen hedef

    void Start()
    {
        // Başlangıçta basketbol topunu takip et
        currentTarget = playerTarget;
    }

    void LateUpdate()
    {
        if (currentTarget != null)
        {
            // Kameranın hedef pozisyonu
            Vector3 desiredPosition = currentTarget.position + offset;
            // Yumuşak geçiş için interpolasyon
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}