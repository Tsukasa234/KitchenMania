using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveUpKeyText;
    [SerializeField] private TextMeshProUGUI moveDownKeyText;
    [SerializeField] private TextMeshProUGUI moveLeftKeyText;
    [SerializeField] private TextMeshProUGUI moveRightKeyText;
    [SerializeField] private TextMeshProUGUI interactKeyText;
    [SerializeField] private TextMeshProUGUI interactAltKeyText;
    [SerializeField] private TextMeshProUGUI pauseKeyText;
    [SerializeField] private TextMeshProUGUI interactGamepadKeyText;
    [SerializeField] private TextMeshProUGUI interactAltGamepadKeyText;
    [SerializeField] private TextMeshProUGUI pauseGamepadKeyText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        UpdateVisual();
        Show();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountDownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        moveUpKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
        pauseKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        interactGamepadKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
        interactAltGamepadKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteractAlt);
        pauseGamepadKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadPause);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
