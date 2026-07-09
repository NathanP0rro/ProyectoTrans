using UnityEngine;
using UnityEngine.UI;   
using TMPro;
using System.Collections;

public class DialogUi : MonoBehaviour
{
    public static DialogUi instance;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text speakerName;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private Image speakerPortrait;
    [SerializeField] private float typingSpeed = 0.1f;
    private Coroutine typingCoroutine;
    private bool isTyping;
    private string currentDialog;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowDialog(string name, string dialog, Sprite portrait)
    {
        dialogPanel.SetActive(true);
        speakerName.text = name;
        speakerPortrait.sprite = portrait;
        currentDialog = dialog;
        
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(dialog));
    }

    private IEnumerator TypeText(string dialog)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    public bool FinishingTyping()
    {
        if(!isTyping)
        {
            return false;
        }
        StopCoroutine(typingCoroutine);
        dialogText.text = currentDialog;
        isTyping = false;
        return true;
    }

    public void HidePanel()
    {
        dialogPanel.SetActive(false);
    }
}
