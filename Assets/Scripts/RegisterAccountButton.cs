using UnityEngine;
using UnityEngine.SceneManagement;

public class RegisterAccountButton : MonoBehaviour
{
    public void LoadWorkerRegistrationScene()
    {
        SceneManager.LoadScene("RegistrationPage");
    }
    public void ButtonOnlineWorker()
    {
        SceneManager.LoadScene("BookerTable");
    }
}
