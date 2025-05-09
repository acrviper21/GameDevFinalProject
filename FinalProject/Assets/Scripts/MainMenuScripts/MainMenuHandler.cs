using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] List<Button> buttons;
    [SerializeField] Canvas optionMenu;
    [SerializeField] Toggle fullScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void PlayButtonClicked()
    {
        SceneManager.LoadScene("TutorialScene");
        //Debug.Log("Play");
    }
    public void OptionButtonClicked()
    {
        OpenOptionMenu();
        //Debug.Log("Option");
    }
    public void QuitButtonClicked()
    {
        Application.Quit();
        //Debug.Log("Quit");
    }

    public void OpenOptionMenu()
    {
        optionMenu.enabled = true;
    }

    public void CloseOptionMenu()
    {
        optionMenu.enabled = false;
    }
}
