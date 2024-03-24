    using System.Collections;
    using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    public class FoodStorage : MonoBehaviour
    {
        public Transform poinSpawn;
        public int maxQuanlityFoodInStorage = 3;

        public List<SOFood> SOFoods =new List<SOFood>();

   // [HideInInspector]
        public List<GameObject> Foods = new List<GameObject>();

        public void SpamwnFood(int index)
        {
            if(Foods.Count<maxQuanlityFoodInStorage)
            {                                                                       
            GameObject foodObj = Instantiate(SOFoods[index].prefab, poinSpawn.position, Quaternion.identity);
                foodObj.transform.parent = poinSpawn;
                Foods.Add(foodObj);
            }    
        }

        public void ReduceFood()
        {
        if (Foods.Count > 0)
        {
            try
            {
                Destroy(Foods[0]);
                Foods.RemoveAt(0);
            }
            catch
            {
                ReduceFood();
            }
        }
        


    }

    }
