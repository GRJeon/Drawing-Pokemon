using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    public GameObject[] cursors;

    private int selectedIndex;
    public bool isSelected = false;

    public void Show()
    {
        isSelected = false;
        selectedIndex = 0;

        UpdateCursor();

        StartCoroutine(SelectCoroutine());
    }

    private IEnumerator SelectCoroutine()
    {
        while (!Input.GetKeyDown(KeyCode.E))
        {
            float input = Input.GetAxisRaw("Vertical");
            if (input > 0)
            {
                if (--selectedIndex < 0) selectedIndex = cursors.Length - 1;
                UpdateCursor();
                yield return new WaitForSeconds(0.5f);
            }
            else if (input < 0)
            {
                if (++selectedIndex == cursors.Length) selectedIndex = 0;
                UpdateCursor();
                yield return new WaitForSeconds(0.25f);
            }

            yield return null;
        }

        if(selectedIndex == 0)
        {
            UIManager.Instance.ShowPokemonPanel();
        }else if(selectedIndex == 1)
        {
            GameManager.Instance.ExitGame();
        }

        isSelected = true;
    }

    private void OnDisable()
    {
        StopCoroutine(SelectCoroutine());
    }

    private void UpdateCursor()
    {

        for (int i = 0; i < cursors.Length; i++)
        {
            if (i == selectedIndex) cursors[i].SetActive(true);
            else cursors[i].SetActive(false);
        }
    }
}
