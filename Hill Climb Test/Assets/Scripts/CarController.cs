using TMPro;
using UnityEngine;
using static UnityEngine.Vector2;

public class CarController : MonoBehaviour
{
    [SerializeField] private float accelerationPower = 200f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Rigidbody2D carRigidbody;
    private bool isAccelerating = false;
    private bool isBraking = false;
    [SerializeField] private TextMeshProUGUI gameOver;

    // Методы для установки состояния ускорения и торможения
    public void SetAccelerating(bool state)
    {
        isAccelerating = state;
    }

    public void SetBraking(bool state)
    {
        isBraking = state;
    }

    private void FixedUpdate()
    {
        // Вызывается, когда кнопка ускорения нажата
        if (isAccelerating)
        {
            Accelerate();
        }

        // Вызывается, когда кнопка торможения нажата
        if (isBraking)
        {
            Brake();
        }
    }

    private void Accelerate()
    {
        // Ускорение и поворот вперед
        carRigidbody.AddForce(transform.right * accelerationPower);
        carRigidbody.rotation -= rotationSpeed * Time.fixedDeltaTime;
    }

    private void Brake()
    {
        // Торможение и поворот назад
        carRigidbody.AddForce(-transform.right * (accelerationPower / 2)); // Меньшая сила для торможения
        carRigidbody.rotation += rotationSpeed * Time.fixedDeltaTime;
    }
    private void Update()
    {
        // Handle keyboard inputs
        if (Input.GetKey(KeyCode.RightArrow))
        {
            SetAccelerating(true);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            SetAccelerating(false);
        }
        
        Vector3 carInView = Camera.main.WorldToViewportPoint(carRigidbody.position);
        if (carInView.x < 0) {
            // Машина за пределами экрана, остановите игру
            GameOver();
        }
        
        float zAngle = transform.eulerAngles.z;
        if (zAngle > 120 && zAngle < 180) {
            // Машина перевернута, остановите игру
            GameOver();
        }
    }
    
    private void GameOver() {
        // Остановите все движения машины
        carRigidbody.velocity = zero;
        carRigidbody.angularVelocity = 0;

        gameOver.gameObject.SetActive(true);
        // Остановите время игры, если нужно
        Time.timeScale = 0;
    }

}