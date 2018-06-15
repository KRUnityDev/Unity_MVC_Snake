using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISnakeView : MonoBehaviour, SnakeView {

    SnakeModel model;
    RectTransform rectTransform;

    [SerializeField]
    GameObject snakeCellPrefab;
    [SerializeField]
    float spacing = 5;

    // Use this for initialization
    void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void GenerateMap()
    {
        Vector2 cellSize = rectTransform.sizeDelta.x + 
        for (int y = 0; y < model.MapHeight; y++)
        {
            for(int x = 0; x < model.MapWidth; x++)
            {
                
            }
        }
    }

    public void ChangeModel(SnakeModel model)
    {
        this.model = model;
    }

    public void InitializeView()
    {
        throw new System.NotImplementedException();
    }

    public void SetModel(SnakeModel model)
    {
        this.model = model;
    }

    public void UpdateView()
    {
        
    }
}
