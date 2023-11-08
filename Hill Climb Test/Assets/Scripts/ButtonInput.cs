using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonInput : MonoBehaviour
{
    [SerializeField] private CarController carController;
    [SerializeField] private bool isAccelerateButton;

    // Вызывается, когда пользователь нажимает на кнопку
    public void OnPress()
    {
        if (isAccelerateButton)
            carController.SetAccelerating(true); // начать ускорение
        else
            carController.SetBraking(true); // начать торможение
    }

    // Вызывается, когда пользователь отпускает кнопку
    public void OnRelease()
    {
        if (isAccelerateButton)
            carController.SetAccelerating(false); // прекратить ускорение
        else
            carController.SetBraking(false); // прекратить торможение
    }
}