using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsEnemies : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemy;

    [SerializeField]
    private Vector2 _direction;

    private List<Transform> _spawnedEnemies;


    [SerializeField]
    private float _timeBetweenSpawnsSeconds = 1.0f;

    [SerializeField]
    private float yRange = 1.0f;

    [SerializeField]
    private Bounds _bounds;

    // Start is called before the first frame update
    private void Start()
    {
        _spawnedEnemies = new List<Transform>();
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    private void Update()
    {
        var deletedEnemies = new List<Transform>();
        foreach (var enemy in _spawnedEnemies)
        {
            if (!_bounds.Contains(enemy.position))
            {
                deletedEnemies.Add(enemy);
            }
        }

        foreach (var deletedEnemy in deletedEnemies)
        {
            _spawnedEnemies.Remove(deletedEnemy);
            Destroy(deletedEnemy.gameObject);
        }
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            var offset = (float)(2.0 * new System.Random().NextDouble() - 1.0) * yRange;
            var enemy = Instantiate(_enemy, new Vector3(transform.position.x, transform.position.y + offset, transform.position.z), Quaternion.identity);
            enemy.GetComponent<Flying>().Direction = _direction;
            _spawnedEnemies.Add(enemy.transform);
            yield return new WaitForSeconds(_timeBetweenSpawnsSeconds);
        }

    }
}
