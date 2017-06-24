using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialize : MonoBehaviour
{
    private void LateUpdate()
    {
        SceneManager.LoadScene(1);
    }
}
