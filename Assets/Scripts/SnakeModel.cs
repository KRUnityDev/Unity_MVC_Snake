using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeModel : MonoBehaviour {

    //Basic snake stuff
    private Vector2Int snakePosition = new Vector2Int(2, 2);
    private int snakePoints = 4;
    private Vector2Int snakeMovementDirection = new Vector2Int(0, 1);
    private Vector2Int applePosition = new Vector2Int(5, 5);

    public enum cellTypes
    {
        empty,
        snake,
        apple
    }

    Cell[] cells;

    private int mapWidth = 10;
    private int mapHeight = 10;


    public float timer = 0f;
    public float timeBetweenSnakeUpdates = 0.25f;

    public List<Component> views;
    public Component controller;


    public struct Cell
    {
        cellTypes cellType;
        int cellLife;

        public int CellLife
        {
            get
            {
                return cellLife;
            }

            set
            {
                cellLife = value;
            }
        }

        public cellTypes CellType
        {
            get
            {
                return cellType;
            }

            set
            {
                cellType = value;
            }
        }
    }


    public Vector2Int SnakePosition
    {
        get
        {
            return snakePosition;
        }

        set
        {
            snakePosition = value;
        }
    }

    public int SnakePoints
    {
        get
        {
            return snakePoints;
        }

        set
        {
            snakePoints = value;
        }
    }

    public Vector2Int SnakeMovementDirection
    {
        get
        {
            return snakeMovementDirection;
        }

        set
        {
            snakeMovementDirection = value;
        }
    }

    public int MapWidth
    {
        get
        {
            return mapWidth;
        }

        set
        {
            mapWidth = value;
        }
    }

    public int MapHeight
    {
        get
        {
            return mapHeight;
        }

        set
        {
            mapHeight = value;
        }
    }

    public Cell[] Cells
    {
        get
        {
            return cells;
        }

        set
        {
            cells = value;
        }
    }

    // Use this for initialization
    void Awake()
    {
        CreateMap();
        foreach (Component view in views)
        {
            view.GetComponent<SnakeView>().SetModel(this);
            view.GetComponent<SnakeView>().InitializeView();
        }
        (this.controller as SnakeController).ChangeModel(this);
        GenerateNewApple();
        SnakeUpdate();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= timeBetweenSnakeUpdates)
        {
            SnakeUpdate();
            timer = 0f;
        }
        else timer += Time.deltaTime;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    void CreateMap()
    {
        Cells = new Cell[MapWidth * MapHeight];
        for (int y = 0; y < MapHeight; y++)
        {
            for (int x = 0; x < MapWidth; x++)
            {
                Cells[y * MapHeight + x] = new Cell();
                Cells[y * MapHeight + x].CellLife = 0;
                Cells[y * MapHeight + x].CellType = cellTypes.empty;
            }
        }
    }


    void SnakeUpdate()
    {
        snakePosition += snakeMovementDirection;
        if (snakePosition.x > MapWidth - 1) snakePosition.x = 0;
        if (snakePosition.x < 0) snakePosition.x = MapWidth - 1;
        if (snakePosition.y > MapHeight - 1) snakePosition.y = 0;
        if (snakePosition.y < 0) snakePosition.y = MapWidth - 1;

        Cells[snakePosition.y * MapHeight + snakePosition.x].CellLife += snakePoints;

        if (Cells[snakePosition.y * MapHeight + snakePosition.x].CellType == cellTypes.apple)
        {
            snakePoints++;
            GenerateNewApple();
        }
        Cells[snakePosition.y * MapHeight + snakePosition.x].CellType = cellTypes.snake;

        for (int y = 0; y < MapHeight; y++)
        {
            for (int x = 0; x < MapWidth; x++)
            {
                if (Cells[y * MapHeight + x].CellType == cellTypes.snake)
                {
                    if (Cells[y * MapHeight + x].CellLife > SnakePoints)
                    {
                        EndGame();
                        break;
                    }
                    if (Cells[y * MapHeight + x].CellLife > 1) Cells[y * MapHeight + x].CellLife -= 1;
                    else
                    {
                        Cells[y * MapHeight + x].CellLife = 0;
                        Cells[y * MapHeight + x].CellType = cellTypes.empty;
                    }
                }
            }
        }

        UpdateViews();
    }

    void UpdateViews()
    {
        foreach(Component view in views)
        {
            view.GetComponent<SnakeView>().UpdateView();
        }
    }

    public void SetMovementDirection(Vector2Int newMovementDirection)
    {
        snakeMovementDirection = newMovementDirection;
    }

    void GenerateNewApple()
    {
        Vector2Int newApplePosition;
        while (true)
        {
            newApplePosition = new Vector2Int(Random.Range(0, MapWidth - 1), Random.Range(0, MapHeight - 1));
            if(Cells[newApplePosition.x * MapHeight + newApplePosition.y].CellLife <= 0)
            {
                break;
            }
        }
        Cells[newApplePosition.x * MapHeight + newApplePosition.y].CellType = cellTypes.apple;
    }

    void EndGame()
    {
        foreach (Component view in views)
        {
            view.GetComponent<SnakeView>().GameEnds();
        }
    }

}
