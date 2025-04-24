using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Game : MonoBehaviour
{
    private int height = BoardConst.HEIGHT;
    private int width = BoardConst.WIDTH;
    private int nextMoveX, nextMoveY;
    private bool[,] horizontalWalls; // có tường ngang hay không tại vị trí x,y
    private bool[,] verticalWalls; // có tường dọc hay không tại vị trí x,y
    private List<Vector2Int> path;  // Để lưu đường đi tìm được
    [SerializeField] private BackGround bg;
    [SerializeField] private Tile tile;
    [SerializeField] private Wall wall;
    [SerializeField] private Transform level;
    [SerializeField] private Player player;
    [SerializeField] private Goal goal;
    [SerializeField] private PathMaze pathMaze;

    private float offSet = 0.012f;
    private CellStatus[,] status;


    void Start()
    {

        SetStartMove();
        InitSeedState();
        SpawnPlayerAndGoal();
        ClearLevel();
        GenerateMaze();
        SubscribeEvent();
    }



    //TOP_LEFT
    void SetStartMove()
    {
        nextMoveX = 0;
        nextMoveY = height - 1;
    }

    void SubscribeEvent()
    {
        EventObserveManager.Instance.Subscribe(EventNameConst.MOVE, MoveEvent);
        EventObserveManager.Instance.Subscribe(EventNameConst.SHOW_PATH, ShowPath);
        EventObserveManager.Instance.Subscribe(EventNameConst.AUTO_MOVE, AutoMove);
    }
    void UnscribeEvent()
    {
        EventObserveManager.Instance.Unsubscribe(EventNameConst.MOVE, MoveEvent);
        EventObserveManager.Instance.Unsubscribe(EventNameConst.SHOW_PATH, ShowPath);
        EventObserveManager.Instance.Unsubscribe(EventNameConst.AUTO_MOVE, AutoMove);
    }
    void OnDestroy()
    {
        UnscribeEvent();
    }
    void InitSeedState()
    {
        Random.InitState(StageManager.Instance.currentStage.seed);
    }


    void ClearLevel()
    {
        foreach (Transform child in level)
        {
            Destroy(child.gameObject);
        }
        horizontalWalls = new bool[width + 1, height];
        verticalWalls = new bool[width, height + 1];
        status = new CellStatus[width, height];
    }


    public void GenerateMaze()
    {
        DFS(0, 0);
    }

    private void DFS(int xCell, int yCell)
    {
        status[xCell, yCell] = CellStatus.Visiting;
        Instantiate(tile, new Vector3(xCell, yCell), Quaternion.identity, level);
        var directions = new[]
        {
            // x of current cell, y of current cell, wallContainer, x of wall, y of wall, direction, angle of wall prefab
            (xCell - 1, yCell, horizontalWalls, xCell, yCell, Vector3.right, 90),
            (xCell + 1, yCell, horizontalWalls, xCell + 1, yCell, Vector3.right, 90),
            (xCell, yCell - 1, verticalWalls, xCell, yCell, Vector3.up, 0),
            (xCell, yCell + 1, verticalWalls, xCell, yCell + 1, Vector3.up, 0),
        };


        //Bắt đầu tìm đường bắt đầu theo những hướng khác nhau
        foreach (var (_xNextCell, _yNextCell, wallArray, xWall, yWall, direction, angle) in directions.OrderBy(d => Random.value))
        {
            // Nếu ô kế bên nằm ngoài ranh giới mê cung hoặc ô kế bên này đã thăm + có thể tạo tường?
            if (!(0 <= _xNextCell && _xNextCell < width && 0 <= _yNextCell && _yNextCell < height) || (status[_xNextCell, _yNextCell] == CellStatus.Visited && Random.value > offSet))
            {
                wallArray[xWall, yWall] = true;
                Instantiate(wall, new Vector3(xWall, yWall) - direction / 2, Quaternion.Euler(0, 0, angle), level);
            }
            else if (status[_xNextCell, _yNextCell] == CellStatus.Unvisited)
            {
                DFS(_xNextCell, _yNextCell);
            }
        }
        status[xCell, yCell] = CellStatus.Visited;
    }


    void SpawnPlayerAndGoal()
    {
        player.SetPosition(0, height - 1);
        do
            goal.SetPosition(Random.Range(0, width), Random.Range(0, height));
        while (Vector3.Distance(player.GetPosition(), goal.GetPosition()) < (width + height) / 4);
    }

    void MoveEvent(object data)
    {
        DirectionType directionControl = (DirectionType)data;
        var directionFromPlayer = new[]
        {
            (player.GetCellX() - 1, player.GetCellY(), horizontalWalls, player.GetCellX(), player.GetCellY(),DirectionType.LEFT),
            (player.GetCellX() + 1, player.GetCellY(), horizontalWalls, player.GetCellX() + 1, player.GetCellY(),DirectionType.RIGHT),
            (player.GetCellX(), player.GetCellY() - 1, verticalWalls, player.x, player.GetCellY(), DirectionType.DOWN),
            (player.GetCellX(), player.GetCellY() + 1, verticalWalls, player.x, player.GetCellY() + 1,DirectionType.UP),
        };

        foreach (var (xSide, ySide, wallContainer, xWall, yWall, k) in directionFromPlayer)
        {
            if (k == directionControl)
            {
                if (!wallContainer[xWall, yWall])
                {
                    SetNextMove(xSide, ySide);
                    break;
                }
            }
        }
    }
    void Update()
    {
        player.MoveAndRotate(nextMoveX, nextMoveY);
        CheckWin();
    }
    void CheckWin()
    {
        if (Vector3.Distance(player.GetPosition(), goal.GetPosition()) < 0.12f)
        {
            AudioManager.Instance.PlaySoundEffect(SoundType.Win);
            StageManager.Instance.CompleteCurrentStage(3);
            StageManager.Instance.LoadNextStage();
        }
    }
    void SetNextMove(int newX, int newY)
    {
        nextMoveX = newX;
        nextMoveY = newY;
    }


    //SHOW HINT PATH
    public void ShowPath(object data)
    {
        Vector2Int start = new Vector2Int(player.GetCellX(),player.GetCellY());
        Vector2Int end = Vector2Int.RoundToInt(goal.GetPosition());

   
        path = FindPathBFS(start, end);

        if (path != null && path.Count > 0)
        {

            pathMaze.DrawPath(path, Color.red);
        }
        else
        {
            Debug.Log("Không tìm thấy đường đi!");
        }
    }

    //PLAYER AUTO MOVE TO GOAL
    public void AutoMove(object data)
    {
        ShowPath(null);
        if (path != null && path.Count > 0)
        {
            StartCoroutine(MovePlayerAlongPath(path));
        }
    }

    IEnumerator MovePlayerAlongPath(List<Vector2Int> path)
    {
        foreach (var target in path)
        {
            nextMoveX = target.x;
            nextMoveY = target.y;
            yield return new WaitForSeconds(0.1f);
        }
        CheckWin();
    }

    List<Vector2Int> FindPathBFS(Vector2Int start, Vector2Int end)
    {
        Queue<List<Vector2Int>> openList = new Queue<List<Vector2Int>>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        // Bắt đầu với một đường đi chỉ có start
        openList.Enqueue(new List<Vector2Int> { start });
        visited.Add(start);

        while (openList.Count > 0)
        {
            List<Vector2Int> currentPath = openList.Dequeue();
            Vector2Int current = currentPath[currentPath.Count - 1];

            // Nếu đạt đến đích, trả về đường đi
            if (current == end)
            {
                return currentPath;
            }

            // Kiểm tra các ô xung quanh
            var neighbors = GetNeighbors(current);
            foreach (var neighbor in neighbors)
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    List<Vector2Int> newPath = new List<Vector2Int>(currentPath) { neighbor };
                    openList.Enqueue(newPath);
                }
            }
        }

        return null;  // Nếu không tìm thấy đường đi
    }

    // Lấy các ô xung quanh ô hiện tại
    List<Vector2Int> GetNeighbors(Vector2Int current)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        int cx = current.x, cy = current.y;

        // Kiểm tra các ô xung quanh (điều kiện biên và không có tường)
        if (cx > 0 && !horizontalWalls[cx, cy]) neighbors.Add(new Vector2Int(cx - 1, cy));  // Left
        if (cx < width - 1 && !horizontalWalls[cx + 1, cy]) neighbors.Add(new Vector2Int(cx + 1, cy));  // Right
        if (cy > 0 && !verticalWalls[cx, cy]) neighbors.Add(new Vector2Int(cx, cy - 1));  // Down
        if (cy < height - 1 && !verticalWalls[cx, cy + 1]) neighbors.Add(new Vector2Int(cx, cy + 1));  // Up

        return neighbors;
    }

}
