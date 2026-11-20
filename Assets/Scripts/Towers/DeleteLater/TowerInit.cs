using UnityEngine;

public class TowerInit : MonoBehaviour
{
    [SerializeField] TowerTypes types;
    [SerializeField] int level;
    [SerializeField] BulletFactory bulletFactory;
    void Start()
    {
        TowerTargetingSystem tower = GetComponent<TowerTargetingSystem>();
        tower.Initialize(TowerFlyWeightPointer.Get(types, level), bulletFactory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
