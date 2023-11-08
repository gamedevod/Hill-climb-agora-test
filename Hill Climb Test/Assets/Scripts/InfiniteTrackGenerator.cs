using UnityEngine;
using System.Collections.Generic;

public class InfiniteTrackGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> segmentPrefabs; // Список префабов сегментов
    [SerializeField] private float segmentSpawnOffset = 10f; // Смещение от правого края экрана для спавна нового сегмента
    [SerializeField] private int initialSegments = 5; // Количество начальных сегментов

    private Queue<GameObject> segmentPool = new Queue<GameObject>(); // Очередь для object pooling
    private List<GameObject> activeSegments = new List<GameObject>(); // Активные сегменты на сцене
    private float nextSpawnX; // Позиция на оси X для спавна следующего сегмента

    void Start()
    {
        nextSpawnX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x; // Начальная позиция первого сегмента

        for (int i = 0; i < initialSegments; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        float cameraRightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        // Проверяем, нужно ли спавнить новый сегмент
        if (nextSpawnX - cameraRightEdge < segmentSpawnOffset)
        {
            SpawnSegment();
        }

        // Проверяем, нужно ли перерабатывать сегменты
        CheckSegmentsForRecycle();
    }

    private void SpawnSegment()
    {
        GameObject segment = segmentPool.Count > 0 ? segmentPool.Dequeue() : Instantiate(segmentPrefabs[Random.Range(0, segmentPrefabs.Count)]);
        segment.SetActive(true);

        // Вычисляем длину сегмента
        float segmentLength = segment.GetComponent<Collider2D>().bounds.size.x;
        // Спавним новый сегмент на следующей позиции
        segment.transform.position = new Vector3(nextSpawnX + segmentLength / 2, 0, 0);
        // Обновляем nextSpawnX для следующего сегмента
        nextSpawnX += segmentLength;

        activeSegments.Add(segment);
    }

    private void CheckSegmentsForRecycle()
    {
        float cameraLeftEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;

        while (activeSegments.Count > 0)
        {
            GameObject segment = activeSegments[0];
            float segmentRightEdge = segment.transform.position.x + segment.GetComponent<Collider2D>().bounds.size.x / 2;
            
            // Перерабатываем сегменты, которые полностью вышли за левый край экрана
            if (segmentRightEdge < cameraLeftEdge)
            {
                activeSegments.RemoveAt(0);
                segment.SetActive(false);
                segmentPool.Enqueue(segment);
            }
            else
            {
                // Если самый старый сегмент еще виден, прекращаем проверку
                break;
            }
        }
    }
}
