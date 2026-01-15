using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Kéo UI Text vào đây
    public GameObject dialogueBox;       // Kéo UI Image (hộp thoại) vào đây
    private Queue<string> sentences;     // Hàng đợi các câu thoại

    void Start()
    {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false); // Ẩn hộp thoại khi bắt đầu
    }

    // Hàm bắt đầu hội thoại
    public void StartDialogue(string[] dialogueLines)
    {
        dialogueBox.SetActive(true);
        sentences.Clear();

        foreach (string line in dialogueLines)
        {
            sentences.Enqueue(line);
        }

        DisplayNextSentence();
    }

    // Hàm hiện câu tiếp theo
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence; // Gán chữ lên màn hình
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        Debug.Log("Kết thúc hội thoại.");
    }
}
