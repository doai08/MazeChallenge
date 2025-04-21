using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public int height, width;
    private int x, y;
    public bool[,] horizontalWalls;
    public bool[,] verticalWalls;
    public Transform bg;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject wall;

    [SerializeField] private Transform level;
    [SerializeField] private Transform player;
    [SerializeField] private Transform goal;

    public float offSet;
    public Camera cam;
    public LineRenderer lineRenderer;

    private int[,] status;

    [Header("Mobile Buttons")]
    public Button upButton, downButton, leftButton, rightButton;

    void Start()
    {
        cam.transform.position = new Vector3(4.45f, 6.65f, -10);
        bg.position = new Vector3(4.45f, 6.65f);

        foreach (Transform child in level)
            Destroy(child.gameObject);

        horizontalWalls = new bool[width + 1, height];
        verticalWalls = new bool[width, height + 1];
        status = new int[width, height];

        void dfs(int x, int y)
        {
            status[x, y] = 1;
            Instantiate(floor, new Vector3(x, y), Quaternion.identity, level);

            var directions = new[]
            {
                (x - 1, y, horizontalWalls, x, y, Vector3.right, 90, KeyCode.A),
                (x + 1, y, horizontalWalls, x + 1, y, Vector3.right, 90, KeyCode.D),
                (x, y - 1, verticalWalls, x, y, Vector3.up, 0, KeyCode.S),
                (x, y + 1, verticalWalls, x, y + 1, Vector3.up, 0, KeyCode.W),
            };

            foreach (var (nx, ny, wallArray, wx, wy, dir, angle, _) in directions.OrderBy(d => Random.value))
            {
                if (!(0 <= nx && nx < width && 0 <= ny && ny < height) || (status[nx, ny] == 2 && Random.value > offSet))
                {
                    wallArray[wx, wy] = true;
                    Instantiate(wall, new Vector3(wx, wy) - dir / 2, Quaternion.Euler(0, 0, angle), level);
                }
                else if (status[nx, ny] == 0)
                {
                    dfs(nx, ny);
                }
            }

            status[x, y] = 2;
        }

        dfs(0, 0);

        x = Random.Range(0, width);
        y = Random.Range(0, height);
        player.position = new Vector3(x, y);

        do goal.position = new Vector3(Random.Range(0, width), Random.Range(0, height));
        while (Vector3.Distance(player.position, goal.position) < (width + height) / 4);

        DrawPath();

        // Gán sự kiện cho mobile buttons
        if (upButton != null) upButton.onClick.AddListener(() => TryMove(x, y + 1));
        if (downButton != null) downButton.onClick.AddListener(() => TryMove(x, y - 1));
        if (leftButton != null) leftButton.onClick.AddListener(() => TryMove(x - 1, y));
        if (rightButton != null) rightButton.onClick.AddListener(() => TryMove(x + 1, y));

#if UNITY_ANDROID || UNITY_IOS
        SetMobileButtonsActive(true);
#else
        SetMobileButtonsActive(false);
#endif
    }

    void Update()
    {
        var dirs = new[]
        {
            (x - 1, y, horizontalWalls, x, y, Vector3.right, 90, KeyCode.LeftArrow),
            (x + 1, y, horizontalWalls, x + 1, y, Vector3.right, 90, KeyCode.RightArrow),
            (x, y - 1, verticalWalls, x, y, Vector3.up, 0, KeyCode.DownArrow),
            (x, y + 1, verticalWalls, x, y + 1, Vector3.up, 0, KeyCode.UpArrow),
        };

        foreach (var (nx, ny, wall, wx, wy, _, _, k) in dirs)
        {
            if (Input.GetKeyDown(k))
            {
                if (!wall[wx, wy])
                {
                    TryMove(nx, ny);
                }
            }
        }

        player.position = Vector3.Lerp(player.position, new Vector3(x, y), Time.deltaTime * 12);

        if (Vector3.Distance(player.position, goal.position) < 0.12f)
        {
            Start();
        }
    }

    void TryMove(int newX, int newY)
    {
        if (IsValidMove(newX, newY))
        {
            x = newX;
            y = newY;
            //DrawPath();
        }
    }

    bool IsValidMove(int newX, int newY)
    {
        if (newX < 0 || newX >= width || newY < 0 || newY >= height)
            return false;

        if (newX < x && horizontalWalls[x, y]) return false;
        if (newX > x && horizontalWalls[x + 1, y]) return false;
        if (newY < y && verticalWalls[x, y]) return false;
        if (newY > y && verticalWalls[x, y + 1]) return false;

        return true;
    }

    Vector2Int GetPlayerCellPosition()
    {
        int cellX = Mathf.FloorToInt(player.position.x);
        int cellY = Mathf.FloorToInt(player.position.y);
        return new Vector2Int(cellX, cellY);
    }

    void DrawPath()
    {
        Vector2Int start = GetPlayerCellPosition();
        Vector2Int end = Vector2Int.RoundToInt(goal.position);
        List<Vector3> path = FindPathDFS(start, end).Select(p => new Vector3(p.x, p.y)).ToList();

        lineRenderer.positionCount = path.Count;
        lineRenderer.SetPositions(path.ToArray());
    }

    List<Vector2Int> FindPathDFS(Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> path = new();
        HashSet<Vector2Int> visited = new();

        bool DFS(Vector2Int current)
        {
            if (visited.Contains(current)) return false;
            visited.Add(current);
            path.Add(current);

            if (current == end) return true;

            int cx = current.x, cy = current.y;
            var neighbors = new List<Vector2Int>();

            if (cx > 0 && !horizontalWalls[cx, cy]) neighbors.Add(new Vector2Int(cx - 1, cy));
            if (cx < width - 1 && !horizontalWalls[cx + 1, cy]) neighbors.Add(new Vector2Int(cx + 1, cy));
            if (cy > 0 && !verticalWalls[cx, cy]) neighbors.Add(new Vector2Int(cx, cy - 1));
            if (cy < height - 1 && !verticalWalls[cx, cy + 1]) neighbors.Add(new Vector2Int(cx, cy + 1));

            foreach (var next in neighbors)
            {
                if (DFS(next)) return true;
            }

            path.RemoveAt(path.Count - 1);
            return false;
        }

        DFS(start);
        return path;
    }

    void SetMobileButtonsActive(bool isActive)
    {
        if (upButton) upButton.gameObject.SetActive(isActive);
        if (downButton) downButton.gameObject.SetActive(isActive);
        if (leftButton) leftButton.gameObject.SetActive(isActive);
        if (rightButton) rightButton.gameObject.SetActive(isActive);
    }
}
