using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Fields
        
        [SerializeField] string playerName = "Player Name";
        [SerializeField] int level = 0;
        [SerializeField] int coin = 0;

        #endregion

        #region Properties

        public string Name => playerName;

        public int Level => level;
        public int Coin => coin;

        public Vector3 Position => transform.position;

        #endregion

        #region Save and Load

        public void Save()
        {
            
        }

        public void Load()
        {
            
        }

    #endregion
}
