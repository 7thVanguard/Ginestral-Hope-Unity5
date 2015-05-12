using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUICodexMode : MonoBehaviour
{
    GameObject backButton;
    GameObject image;

    GameObject []table; 


    void Awake()
    {
        GameGUI.codexMode = this.gameObject;

        table = new GameObject[5];

        backButton = transform.parent.FindChild("Codex").FindChild("BTN Back").gameObject;
        image = transform.parent.FindChild("Codex").FindChild("IMG Background").gameObject;
            
        table[0] = transform.parent.FindChild("Codex").FindChild("1st Intance TXT").gameObject;
        table[1] = transform.parent.FindChild("Codex").FindChild("2nd Intance TXT").gameObject;
        table[2] = transform.parent.FindChild("Codex").FindChild("3rd Intance TXT").gameObject;
        table[3] = transform.parent.FindChild("Codex").FindChild("4th Intance TXT").gameObject;
        table[4] = transform.parent.FindChild("Codex").FindChild("5th Intance TXT").gameObject;

        BackButton();
    }


    void Update()
    {

    }


    public void BackButton()
    {
        GameFlow.onInterface = false;

        backButton.SetActive(false);
        image.SetActive(false);

        foreach (GameObject tableObj in table)
            tableObj.SetActive(false);
    }


    public void ActivateTable(int tableNumber)
    {
        GameFlow.onInterface = true;

        backButton.SetActive(true);
        image.SetActive(true);
        table[tableNumber - 1].SetActive(true);
    }
}
