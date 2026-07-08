using UnityEngine;
using TMPro;

public class DialogUi : MonoBehaviour
{

    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text speakerName;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private Sprite speakerPortrait;

    public void ShowDialog(string name, string dialog, Sprite portrait)
    {
        dialogPanel.SetActive(true);
        speakerName.text = name;
        dialogText.text = dialog;
        speakerPortrait = portrait;
    }

    public void HidePanel()
    {
        dialogPanel.SetActive(false);
    }
}
