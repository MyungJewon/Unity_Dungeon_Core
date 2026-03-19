using UnityEngine;

namespace DungeonCore.Characters.Enemy
{
    /// <summary>
    /// 기본 적 클래스. HP와 피격/사망 처리를 담당한다.
    /// </summary>
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private int _maxHp = 100;

        public int CurrentHp { get; private set; }
        public bool IsDead => CurrentHp <= 0;

        private void Awake()
        {
            CurrentHp = _maxHp;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead) return;

            CurrentHp -= damage;
            Debug.Log($"[{gameObject.name}] 피해 {damage} | 남은 HP: {CurrentHp}/{_maxHp}");

            if (IsDead)
                Die();
        }

        private void Die()
        {
            Debug.Log($"[{gameObject.name}] 사망");
            // TODO: 사망 애니메이션 / 드롭 처리
            gameObject.SetActive(false);
        }

        // 히트박스 시각화 (에디터에서만 표시)
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}
