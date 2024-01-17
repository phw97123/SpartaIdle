using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isPressing = false;

    Button button;

    private WaitForSeconds pushTime = new WaitForSeconds(1f);
    private WaitForSeconds repeatTime = new WaitForSeconds(0.3f);

    private void Start()
    {
        button = gameObject.GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
        StartCoroutine(LongPress());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;
    }

    public IEnumerator LongPress()
    {
        yield return pushTime;
        while (isPressing)
        {
            Execute();
            yield return repeatTime;
        }
    }

    private void Execute()
    {
        if (button != null)
        {
            button.onClick.Invoke();
        }
    }
}