using UnityEngine;

public class TrackMover : MonoBehaviour
{
    [SerializeField] private float trackSpeed = 5f; // Скорость перемещения трассы
    public float TrackLength { get; }

    
    public float Length { get; private set; } // Длина сегмента

    void Awake()
    {
        // Инициализация длины сегмента, например, на основе коллайдера или визуальной длины
        Length = CalculateSegmentLength();
    }

    private float CalculateSegmentLength()
    {
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        if (polygonCollider == null)
        {
            Debug.LogError("PolygonCollider2D is not attached to the segment");
            return 0f;
        }
    
        // Вычисляем минимальную и максимальную точки X среди всех точек коллайдера
        float minX = polygonCollider.points[0].x;
        float maxX = polygonCollider.points[0].x;
        foreach (Vector2 point in polygonCollider.points)
        {
            if (point.x < minX) minX = point.x;
            if (point.x > maxX) maxX = point.x;
        }

        // Учитываем локальное положение коллайдера относительно родительского объекта
        float length = maxX - minX;
        length *= transform.localScale.x; // Учитываем масштаб объекта
        return length;
    }
    
    void Update()
    {
        // Перемещаем трассу влево
        transform.Translate(Vector2.left * trackSpeed * Time.deltaTime);
    }
    
}