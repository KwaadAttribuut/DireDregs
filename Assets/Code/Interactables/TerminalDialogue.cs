using UnityEngine;

[CreateAssetMenu(fileName ="NewTerminalDialogue", menuName ="Terminal Dialogue")]
public class TerminalDialogue : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.025f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
}
