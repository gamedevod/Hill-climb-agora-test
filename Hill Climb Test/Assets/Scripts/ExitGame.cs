using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        // Вывести сообщение в консоль (видно только в редакторе)
        Debug.Log("Выход из игры");

        // Закрыть приложение
        Application.Quit();

        // Если игра запущена в редакторе, остановить игровой процесс
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}