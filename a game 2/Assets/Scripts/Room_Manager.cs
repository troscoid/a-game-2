using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Manager : MonoBehaviour
{
    public GameObject room_gameObject;
    public GameObject room_sprite;
    public GameObject obstacle_gameObject;
    public GameObject powerup_gameObject;
    public GameObject[] monsters_gameObject;

    public float room_height;
    public float room_width;
    private int room_grid_width = 12;
    private int room_grid_height = 6;
    private float adjust_size_x = 1.9f;
    private float adjust_size_y = 1.8f;
    private float x_offset = 0.86f;
    private float y_offset = 0.9f;

    private float obstacle_randomizer = 0.1f;
    private float monster_randomizer = 0.07f;
    private float powerup_randomizer = 0.05f;

    void Start()
    {
        // Create the array for grid Centers.
        Vector2[] gridCenters = new Vector2[room_grid_height * room_grid_width];
        
        // Get room height and width, multiply by scale and add offset.
        room_height = room_sprite.GetComponent<SpriteRenderer>().size.y * room_gameObject.transform.localScale.y - adjust_size_y;
        room_width = room_sprite.GetComponent<SpriteRenderer>().size.x * room_gameObject.transform.localScale.x - adjust_size_x;

        // Calculate cell size in the grid to get equally spaced centers.
        float grid_cell_height = room_height / room_grid_height;
        float grid_cell_width = room_width / room_grid_width;

        int a = 0;
        // Loop through rows.
        for (int i = 0; i < room_grid_height; i++)
        {
            // Loop through columns.
            for (int j = 0; j < room_grid_width; j++)
            {
                gridCenters[a] = new Vector2((j + 0.5f) * grid_cell_width + x_offset, (i + 0.5f) * grid_cell_height + y_offset);

                bool spawnedObstacle = false;

                // Using obstacle_randomizer, determine if an obstacle should spawn.
                if (Random.Range(0f, 1f) > 1 - obstacle_randomizer)
                {
                    GameObject tempGameObject = Instantiate(obstacle_gameObject, gridCenters[a], Quaternion.identity);
                    tempGameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                    spawnedObstacle = true;
                }

                // Using monster_randomizer, determine if a monster should spawn.
                if (Random.Range(0f, 1f) > 1 - monster_randomizer && !spawnedObstacle)
                {
                    switch (Random.Range(1, 3))
                    {
                        case 1:
                            GameObject tempGameObject = Instantiate(monsters_gameObject[0], gridCenters[a], Quaternion.identity);
                            tempGameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                            break;
                        case 2:
                            tempGameObject = Instantiate(monsters_gameObject[1], gridCenters[a], Quaternion.identity);
                            tempGameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                            break;
                        case 3:
                            tempGameObject = Instantiate(monsters_gameObject[2], gridCenters[a], Quaternion.identity);
                            tempGameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                            break;
                    }
                }

                // Using powerup_randomizer, determine if a monster should spawn.
                if (Random.Range(0f, 1f) > 1 - powerup_randomizer && !spawnedObstacle)
                {
                    GameObject tempGameObject = Instantiate(powerup_gameObject, gridCenters[a], Quaternion.identity);
                }
                a++;
            }
        }
    }
}
