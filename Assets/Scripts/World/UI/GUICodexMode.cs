using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUICodexMode : MonoBehaviour
{
    GameObject backButton;

    GameObject table1;
    GameObject table2;
    GameObject table3;
    GameObject table4;
    GameObject table5;


    void Awake()
    {
        backButton = transform.parent.FindChild("Codex").FindChild("BTN Back").gameObject;

        table1 = transform.parent.FindChild("Codex").FindChild("1st Intance TXT").gameObject;
    }


    void Update()
    {

    }


    public void BackButton()
    {
        GameFlow.onInterface = false;

        backButton.SetActive(false);
        table1.SetActive(false);
    }
}
