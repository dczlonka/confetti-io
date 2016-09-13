using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuPanel : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private InputField nicknameField;

    public string Nickname { get { return nicknameField.text; } set { nicknameField.text = value; } }
    public Button.ButtonClickedEvent OnPlayClicked { get { return playButton.onClick; } }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
