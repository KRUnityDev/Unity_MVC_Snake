using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsSnakeController : MonoBehaviour, SnakeController {

    SnakeModel model;
    Vector2Int snakeMovementDirection;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetButtonDown("SnakeUp"))
        {
            snakeMovementDirection = new Vector2Int(0, 1);
            UpdateModel();
        }
        else if (Input.GetButtonDown("SnakeDown"))
        {
            snakeMovementDirection = new Vector2Int(0, -1);
            UpdateModel();
        }
        else if (Input.GetButton("SnakeLeft"))
        {
            snakeMovementDirection = new Vector2Int(-1, 0);
            UpdateModel();
        }
        else if (Input.GetButton("SnakeRight"))
        {
            snakeMovementDirection = new Vector2Int(1, 0);
            UpdateModel();
        }
    }

    void UpdateModel()
    {
        model.SetMovementDirection(snakeMovementDirection);
    }

    public void ChangeModel(SnakeModel model)
    {
        this.model = model;
    }

    public void SetModel(SnakeModel model)
    {
        this.model = model;
    }

}
