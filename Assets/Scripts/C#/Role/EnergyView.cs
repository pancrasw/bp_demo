using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyView : MonoBehaviour
{
    public GameObject energyItemPrefab;
    List<GameObject> energyItems;
    public int energy
    {
        set
        {
            if (value < 0)
                return;
            if (value > _energy)
            {
                if (value > energyItems.Count)
                {
                    int difference = value - energyItems.Count;
                    for (int i = 0; i < difference; i++)
                    {
                        energyItems.Add(GameObject.Instantiate(energyItemPrefab));
                        energyItems[energyItems.Count - 1].transform.SetParent(transform);
                    }
                }
                for (int i = _energy; i < energyItems.Count && i < value; i++)
                {
                    energyItems[i].SetActive(true);
                }
            }
            else if (value < _energy)
            {
                for (int i = value; i < _energy; i++)
                {
                    energyItems[i].SetActive(false);
                }
            }
            _energy = value;
        }

        get { return _energy; }
    }

    int _energy = 0;

    public void Init(int initialEnergy)
    {
        energyItems = new List<GameObject>();
        energy = initialEnergy;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
