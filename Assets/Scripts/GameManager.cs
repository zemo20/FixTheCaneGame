using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject instruction;
    private TextMeshProUGUI instructionText;
    void Start()
    {
        instructionText = instruction.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame

    public void updateInstruction(string text)
    {
        instructionText.SetText(text);
        instruction.SetActive(true);
    }

    public void removeInstruction()
    {
        instruction.SetActive(false);
    }
}
