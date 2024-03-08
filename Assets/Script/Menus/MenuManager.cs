using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private InputActionReference escape;
    [SerializeField] private Button startBtn;
    [SerializeField] private Slider volumeSlider;


    void Start()
    {
        instance = this;
        Return();
    }

    private void OnEnable()
    {
        escape.action.started += ReturnBack;
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        escape.action.started -= ReturnBack;
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    public void OptionMenu()
    {
        optionMenu.SetActive(true);
        mainMenu.SetActive(false);
        if (Gamepad.current != null)
        {
            volumeSlider.Select();
        }
    }

    public void StartButton()
    {
        GameObject.Find("StartButton").GetComponent<Button>().enabled = false;

        GameObject.Find("SpaceShip").GetComponent<Animator>().SetTrigger("start");
        StartCoroutine("StartButtonColor");
    }


    public void ReturnBack(InputAction.CallbackContext obj)
    {
        Return();
    }

    public void Return()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
        if(Gamepad.current != null)
        {
            startBtn.Select();
        }
    }

    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        {
            if (change == InputDeviceChange.Added && Gamepad.current != null)
            {
                if(mainMenu.activeSelf)
                {
                    startBtn.Select();
                }
                else if (optionMenu.activeSelf)
                {
                    volumeSlider.Select();
                }
            }
            else if (change == InputDeviceChange.Removed && Gamepad.current == null)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator StartButtonColor()
    {
        for(int i = 0; i < 15; i++)
        {
            GameObject.Find("StartButton").GetComponent<Image>().color = new Color(0, 0.78f, 1);
            yield return new WaitForSeconds(0.05f);
            GameObject.Find("StartButton").GetComponent<Image>().color = Color.white;
            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene(1);
    }
}
