using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject LoadingAnim;
    public void LoadScene()
    {
        LoadingAnim.SetActive(true);
        Invoke("LoadNow",0.4f);
    }
    public void LoadNow(){
    SceneManager.LoadScene("GameScene");
    }
}
