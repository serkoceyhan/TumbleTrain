using UnityEngine;

public class Speedometer : MonoBehaviour
{
    public RectTransform needle; // İğnenin RectTransform'ı
    public Rigidbody2D playerRb; // Karakterin Rigidbody2D'si

    public float maxSpeed = 10f; // Maksimum hız
    public float maxNeedleAngle = -180f; // İğnenin maksimum açı değeri

    private void Update()
    {
        UpdateNeedle();
    }

    private void UpdateNeedle()
    {
        float currentSpeed = playerRb.velocity.magnitude; // Anlık hız
        float normalizedSpeed = Mathf.Clamp01(currentSpeed / maxSpeed); // Normalleştirilmiş hız
        float needleAngle = Mathf.Lerp(0, maxNeedleAngle, normalizedSpeed); // Hızdan açıya dönüştürme
        needle.localEulerAngles = new Vector3(0, 0, needleAngle); // İğnenin dönüş açısı
    }
}