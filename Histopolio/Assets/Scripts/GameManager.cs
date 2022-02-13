using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }
    
    // Spawn player on GO Tile
    void SpawnPlayer() {
        player = Instantiate(playerPrefab, new Vector3(8.3f, 0, -3), Quaternion.identity);
        player.name = "Player";
    }
}
