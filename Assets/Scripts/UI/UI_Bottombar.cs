using UnityEngine;
using UnityEngine.UI;

public class UI_Bottombar : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject[] panels;

    private void Start()
    {
        for(int i = 0; i<buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonClicked(index)); 
        }
    }

    private void OnButtonClicked(int index)
    {
        for(int i = 0;i<panels.Length; i++)
        {
            panels[i].SetActive(i == index); 
        }
    }
}
