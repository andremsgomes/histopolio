using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player player;
    private GridManager gridManager;

    [SerializeField] private Player playerPrefab;


    // Start is called before the first frame update
    void Start()
    {
        gridManager = this.GetComponent<GridManager>();

        gridManager.GenerateGrid();
        SpawnPlayer();
    }
    
    // Spawn player on GO Tile
    void SpawnPlayer() {
        player = Instantiate(playerPrefab, new Vector3(8.3f, 0, -3), Quaternion.identity);
        player.name = "Player";
    }
}
