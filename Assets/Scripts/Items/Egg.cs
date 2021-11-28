using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            Destroy(gameObject);
            LevelEndMenu.Show();
            LevelEndMenu.Instance.OpenWinMenu();
        }
    }
}
