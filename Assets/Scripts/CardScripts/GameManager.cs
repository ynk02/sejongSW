using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        InputKey();
    }

    public void StartGame()
    {
        StartCoroutine(DrawManager.Inst.StartDrawCoroutine());
    }

    public void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawManager.onAddCard?.Invoke(true);
        }
    }
}
