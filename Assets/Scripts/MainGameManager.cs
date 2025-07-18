using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public GameObject table;
    public GameObject cell;
    public Canvas gameCanvas;

    // Start is called before the first frame update
    void Start()
    {
        table.SetActive(false);
        cell.SetActive(false);
        gameCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
