using UnityEngine;
using DungeonCore.Characters.Enemy;

namespace DungeonCore.Test
{
    /// <summary>
    /// 테스트 씬용 적 자동 생성기.
    /// 빈 GameObject에 이 컴포넌트를 붙이면 플레이 시 적이 자동으로 배치된다.
    /// </summary>
    public class TestSceneSetup : MonoBehaviour
    {
        [SerializeField] private int _enemyCount = 5;
        [SerializeField] private float _spawnRadius = 4f;

        private void Start()
        {
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                // 원형으로 균등 배치
                float angle = i * (360f / _enemyCount) * Mathf.Deg2Rad;
                Vector3 pos = new Vector3(
                    Mathf.Sin(angle) * _spawnRadius,
                    0f,
                    Mathf.Cos(angle) * _spawnRadius
                );

                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                go.name = $"Enemy_{i + 1}";
                go.transform.position = pos;
                go.AddComponent<EnemyBase>();

                // 구별하기 쉽도록 빨간색 적용
                go.GetComponent<Renderer>().material.color = Color.red;
            }
        }

        // 공격 범위 시각화
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0.5f, 0f, 0.3f);
            Gizmos.DrawWireSphere(Vector3.zero, _spawnRadius);
        }
    }
}
