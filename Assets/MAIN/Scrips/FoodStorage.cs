    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class FoodStorage : MonoBehaviour
    {
        public Transform poinSpawn;
        public int maxQuanlityFoodInStorage = 3;

        public List<SOFood> SOFoods =new List<SOFood>();

    [HideInInspector]
        public List<GameObject> Foods = new List<GameObject>();

        public void SpamwnFood(int index)
        {
            if(Foods.Count<maxQuanlityFoodInStorage)
            {
            Debug.Log("khoa:" + index);
            Debug.Log("khoa2:" + SOFoods[index].name);
            GameObject foodObj = Instantiate(SOFoods[index].prefab, poinSpawn.position, Quaternion.identity);
                foodObj.transform.parent = poinSpawn;
                Foods.Add(foodObj);
            }    
        }

        public void ReduceFood(int index)
        {
            for (int i = 0; i < index; i++)
            {
                GameObject foodObj = Foods[0];
                Foods.RemoveAt(0);
                Destroy(foodObj);
            }
        }

    }
