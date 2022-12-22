using UnityEngine;

public class PlayerValue : MonoBehaviour
{
    public string Name { get; private set; }

    private void Awake()
    {
        Name = "Santa";
    }
}