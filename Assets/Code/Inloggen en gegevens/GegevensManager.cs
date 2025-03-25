using UnityEngine;
using UnityEngine.SceneManagement;

//Deze klasse uiteindelijk uitbreiden voor de info als dat lukt

public class GegevensManager : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene("IntroductieScherm");
    }
}
