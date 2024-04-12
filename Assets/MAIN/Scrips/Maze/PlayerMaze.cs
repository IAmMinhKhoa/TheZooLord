using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMaze : MonoBehaviour
{
    public float moveSpeed = 10f;
    public LayerMask obstacleLayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 newPosition = (Vector2)transform.position + Vector2.left;
            if (CanMoveToPosition(newPosition))
            {
                //currentPos = newPosition;
                MovePlayerToPosition(newPosition);
                //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * moveSpeed);

            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3 newPosition = (Vector2)transform.position + Vector2.right;
            if (CanMoveToPosition(newPosition))
            {
                //currentPos = newPosition;
                MovePlayerToPosition(newPosition);
                //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * moveSpeed);

            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 newPosition = (Vector2)transform.position + Vector2.up;
            if (CanMoveToPosition(newPosition))
            {
                //currentPos = newPosition;
                MovePlayerToPosition(newPosition);
                //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * moveSpeed);

            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 newPosition = (Vector2)transform.position + Vector2.down;
            if (CanMoveToPosition(newPosition))
            {
                //currentPos = newPosition;
                MovePlayerToPosition(newPosition);
                //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * moveSpeed);

            }
        }
    }

    private void MovePlayerToPosition(Vector3 newPosition)
    {
        Debug.Log(newPosition);
        //transform.position = newPosition;
        Vector3 targetPosition = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);
        transform.DOMove(targetPosition, moveSpeed);

    }

    private bool CanMoveToPosition(Vector2 newPosition)
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, Vector2.Distance(transform.position, newPosition), obstacleLayer);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, Vector2.Distance(transform.position, newPosition), obstacleLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, Vector2.Distance(transform.position, newPosition), obstacleLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, Vector2.Distance(transform.position, newPosition), obstacleLayer);

        if (hitUp.collider != null && newPosition.y > transform.position.y)
        {
            Debug.Log(hitUp.collider.gameObject.name);
            return false;
        }
        if (hitDown.collider != null && newPosition.y < transform.position.y)
        {
            Debug.Log(hitDown.collider.gameObject.name);
            return false;
        }
        if (hitRight.collider != null && newPosition.x > transform.position.x)
        {
            Debug.Log(hitRight.collider.gameObject.name);
            return false;
        }
        if (hitLeft.collider != null && newPosition.x < transform.position.x)
        {
            Debug.Log(hitLeft.collider.gameObject.name);
            return false;
        }

        return true;
    }
}
