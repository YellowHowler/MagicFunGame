using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    void Start()
    {
        Ex();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoverEnter()
    {
        Ent();
    }

    public void HoverExit()
    {
        Ex();
    }

    private void Ent()
    {
        startBtn.gameObject.SetActive(true);
    }

    private void Ex()
    {
        startBtn.gameObject.SetActive(true);
    }

    public void StartDuel()
    {
        SceneManager.LoadScene("DuelScene"); 
    }
}
