using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
[RequireComponent(typeof(RectTransform))]

public class UISnakeView : MonoBehaviour, SnakeView {

    SnakeModel model;
    RectTransform rectTransform;

    [SerializeField]
    GameObject snakeCellPrefab;
    [SerializeField]
    Vector2 spacing;
    [SerializeField]
    Color snakeColor = Color.blue;
    [SerializeField]
    Color appleColor = Color.green;
    [SerializeField]
    Color emptyCellColor = Color.grey;

    [SerializeField]
    GameObject GameEndMessage;

    RectTransform[,] cells;

    // Use this for initialization
    void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        if (spacing == null) spacing = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void GenerateMap()
    {
        cells = new RectTransform[model.MapWidth, model.MapHeight];
        Vector2 cellSize = new Vector2(rectTransform.sizeDelta.x / model.MapWidth, rectTransform.sizeDelta.y / model.MapHeight);
        cellSize -= spacing;
        GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup>();
        grid.cellSize = cellSize;
        grid.spacing = spacing;
        grid.constraintCount = model.MapWidth;

        for (int y = model.MapHeight-1; y >= 0; y--)
        {
            for(int x = 0; x < model.MapWidth; x++)
            {
                GameObject snakeCell = Instantiate<GameObject>(snakeCellPrefab, transform);
                cells[x, y] = snakeCell.GetComponent<RectTransform>();
                //RectTransform snakeRectTransform = snakeCell.GetComponent<RectTransform>();
                //snakeRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cellSize.x);
                //snakeRectTransform.SetSizeWithCurrentAnchors(Rect)
            }
        }
    }

    public void ChangeModel(SnakeModel model)
    {
        this.model = model;
    }

    public void InitializeView()
    {
        GenerateMap();
    }

    public void SetModel(SnakeModel model)
    {
        this.model = model;
    }

    public void UpdateView()
    {
        for (int y = 0; y < model.MapHeight; y++)
        {
            for (int x = 0; x < model.MapWidth; x++)
            {
                switch (model.Cells[y * model.MapHeight + x].CellType)
                {
                    case SnakeModel.cellTypes.snake:
                        cells[x, y].GetComponent<Image>().color = snakeColor;
                        break;
                    case SnakeModel.cellTypes.apple:
                        cells[x, y].GetComponent<Image>().color = appleColor;
                        break;
                    case SnakeModel.cellTypes.empty:
                        cells[x, y].GetComponent<Image>().color = emptyCellColor;
                        break;
                }
            }
        }
    }

    public void GameEnds()
    {
        GameEndMessage.SetActive(true);
    }

    public void RestartGame()
    {
        model.RestartGame();
    }
}
