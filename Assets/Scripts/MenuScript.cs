using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        cam.GetComponent<GneralScript>().gamelayer.SetActive(true);
        //cam.GetComponent<GneralScript>().SpawnPlayerController();
        //cam.GetComponent<GneralScript>().SpawnPlayer();
        cam.GetComponent<GneralScript>().Reset();
        cam.GetComponent<GneralScript>().menulayer.SetActive(false);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void RollCredits()
    {
        cam.GetComponent<GneralScript>().creditslayer.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
