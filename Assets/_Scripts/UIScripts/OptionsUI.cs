using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamePadInteractButton;
    [SerializeField] private Button gamepadInteractAltButton;
    [SerializeField] private Button gamepadPauseButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamePadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAltText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;
    [SerializeField] private Transform pressAKeyToRebindButton;

    private Action onClosebuttonAction;


    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();

            UpdateVisual();
        });
        closeButton.onClick.AddListener(() => 
        {
            Hide();
            onClosebuttonAction();
        });

        moveUpButton.onClick.AddListener(() => { RebindingBinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => { RebindingBinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => { RebindingBinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => { RebindingBinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => { RebindingBinding(GameInput.Binding.Interact); });
        interactAltButton.onClick.AddListener(() => { RebindingBinding(GameInput.Binding.InteractAlt); });
        pauseButton.onClick.AddListener(() => { RebindingBinding(GameInput.Binding.Pause); });
        gamePadInteractButton.onClick.AddListener(() => { RebindingBinding(GameInput.Binding.GamepadInteract); });
        gamepadInteractAltButton.onClick.AddListener(() => { RebindingBinding(GameInput.Binding.GamepadInteractAlt); });
        gamepadPauseButton.onClick.AddListener(() => { RebindingBinding(GameInput.Binding.GamepadPause); });

    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UpdateVisual();
        HidePressKeyToRebind();
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10).ToString();
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10).ToString();

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamePadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
        gamepadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteractAlt);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadPause);

    }

    public void Show(Action onCloseButtonAction)
    {
        this.onClosebuttonAction = onCloseButtonAction;
        gameObject.SetActive(true);

        soundEffectsButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressKeyToRebind()
    {
        pressAKeyToRebindButton.gameObject.SetActive(true);
    }
    
    private void HidePressKeyToRebind()
    {
        pressAKeyToRebindButton.gameObject.SetActive(false);
    }

    private void RebindingBinding(GameInput.Binding binding)
    {
        ShowPressKeyToRebind();
        GameInput.Instance.RebindBinding(binding, () => 
        { 
            HidePressKeyToRebind();
            UpdateVisual();
        });
    }
}
