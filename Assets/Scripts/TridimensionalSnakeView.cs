using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridimensionalSnakeView : MonoBehaviour, SnakeView {

    GameObject[] cubes;

    public GameObject cubePrefab;

    public float standardCubeYScale = 1f;
    public float snakeCubeYScale = 1.5f;
    public float appleCubeYScale = 1.25f;

    public float cubeScaleRestoreSpeed = 1f;

    public Vector3 beginPosition;
    public float marginBetweenCubes = 1f;


    public Color standardColor = Color.white;
    public Color snakeColor = Color.blue;
    public Color appleColor = Color.red;
    public float colorChangeSpeed = 1f;

    SnakeModel model;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        for (int y = 0; y < model.MapHeight; y++)
        {
            for (int x = 0; x < model.MapWidth; x++)
            {
                Transform cubeTransform = cubes[y * model.MapHeight + x].transform;
                switch (model.Cells[y * model.MapHeight + x].CellType)
                {
                    case SnakeModel.cellTypes.snake:
                        cubeTransform.localScale = new Vector3(cubeTransform.localScale.x, snakeCubeYScale, cubeTransform.localScale.z);
                        cubeTransform.GetComponent<MeshRenderer>().material.color = snakeColor;
                        break;
                    case SnakeModel.cellTypes.apple:
                        cubeTransform.localScale = new Vector3(cubeTransform.localScale.x, snakeCubeYScale, cubeTransform.localScale.z);
                        cubeTransform.GetComponent<MeshRenderer>().material.color = appleColor;
                        break;
                    case SnakeModel.cellTypes.empty:
                        cubeTransform.localScale = Vector3.Lerp(cubeTransform.localScale, new Vector3(cubeTransform.localScale.x, standardCubeYScale, cubeTransform.localScale.z), Time.deltaTime * cubeScaleRestoreSpeed);
                        cubeTransform.GetComponent<MeshRenderer>().material.color = Color.Lerp(cubeTransform.GetComponent<MeshRenderer>().material.color, standardColor, Time.deltaTime * colorChangeSpeed);
                        break;
                }
            }
        }

    }

    void CreateMap()
    {
        cubes = new GameObject[model.MapWidth * model.MapHeight];
        for (int y = 0; y < model.MapHeight; y++)
        {
            for (int x = 0; x < model.MapWidth; x++)
            {
                cubes[y * model.MapHeight + x] = Instantiate<GameObject>(cubePrefab, new Vector3(beginPosition.x + x * marginBetweenCubes, beginPosition.y, beginPosition.z + y * marginBetweenCubes), Quaternion.identity, transform);
            }
        }
    }

    //SnakeModel interface
    public void ChangeModel(SnakeModel model)
    {
        this.model = model;
    }

    public void SetModel(SnakeModel model)
    {
        this.model = model;
    }


    public void UpdateView()
    {
        //ViewUpdate();
    }
    
    public void InitializeView()
    {
        CreateMap();
    }

    public void GameEnds()
    {

    }

    public void RestartGame()
    {
        model.RestartGame();
    }

}
