using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CutSceneSystem : MonoBehaviour
{
    [SerializeField] DialogUi dialogUIPrefab;
    private DialogUi dialogui;
    public static CutSceneSystem instance;

    private DialogActivator currentScene;
    private int currentLine;
    private bool isPlaying;
    private bool input;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            dialogui = Instantiate(dialogUIPrefab);
            DontDestroyOnLoad(dialogui.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!isPlaying || !input)
            return;

        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Next line pressed");
            NextLine();
        }
    }

    public void StartDialog(DialogActivator currentConversation)
    {
        if (isPlaying)
            return;
        currentScene = currentConversation;
        currentLine = 0;
        isPlaying = true;
        PlayCurrentLine();
    }

    private void PlayCurrentLine()
    {
        DialogLine line = currentScene.lines[currentLine];
        Debug.Log("Showing index: " + currentLine + " Text: " + line.dialog);
        Execute(line);
    }

    private void Execute(DialogLine line)
    {
        if (dialogui == null)
        {
            Debug.LogError("DialogUI is missing!");
            return;
        }
        dialogui.ShowDialog(line.characterName,line.dialog,line.portrait);
        input = true;
    }

    private void NextLine()
    {
        if (dialogui.FinishingTyping())
            return;

        input = false;
        currentLine++;

        if(currentLine >= currentScene.lines.Count)
        {
            EndDialog();
            return;
        }
        PlayCurrentLine();
    }

    private void EndDialog()
    {
        isPlaying = false;
        currentScene = null;
        currentLine = 0;
        dialogui.HidePanel();

    }
}
