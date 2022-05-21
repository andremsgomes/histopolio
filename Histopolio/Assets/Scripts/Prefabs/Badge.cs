using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Badge : MonoBehaviour
{
    [SerializeField] private Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set image
    public void SetImage(Sprite sprite) {
        this.image.sprite = sprite;
    }
}
