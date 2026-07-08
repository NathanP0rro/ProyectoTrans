using Unity.VisualScripting;
using UnityEngine;

public class CutSceneSystem : MonoBehaviour
{
    [SerializeField] DialogUi dialogui;
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
        if(Input.GetMouseButtonDown(0))
        {
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
        Execute(line);
    }

    private void Execute(DialogLine line)
    {
        dialogui.ShowDialog(line.characterName,line.dialog,line.portrait);
        input = true;
    }

    private void NextLine()
    {
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
