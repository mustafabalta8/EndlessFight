using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ground : MonoBehaviour
{
    [SerializeField] Text lifeTimeText;

    private float lifeTime = 15;
    public float LifeTime
    {
        get
        {
            return this.lifeTime;
        }
        set
        {
            this.lifeTime = value;
        }
    }
    void Start()
    {
        GroundController.lastGroundPosition = transform.position;
        
    }

    private void Update()
    {
        //if (Player.gameOn)
        {
            LifeTime -= Time.deltaTime;
            lifeTimeText.text = (int)LifeTime + "";
            if (LifeTime <= 0)
                Destroy(gameObject);
        }
        
    }


}
