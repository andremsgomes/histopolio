using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinPlayer : MonoBehaviour
{
    [SerializeField] private Image avatar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set avatar image
    public void SetAvatar(Sprite avatar) {
        this.avatar.sprite = avatar;
    }
}
