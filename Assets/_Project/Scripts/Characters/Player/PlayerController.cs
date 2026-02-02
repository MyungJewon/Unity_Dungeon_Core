using UnityEngine;
using DungeonCore.FSM;

namespace DungeonCore.Characters.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Settings")]
        public float MoveSpeed = 6f;
        public float RotationSpeed = 15f;

        public CharacterController Controller { get; private set; }
        public Controls InputActions { get; private set; }
        private StateMachine _stateMachine;

        private void Awake()
        {
            Controller = GetComponent<CharacterController>();
            InputActions = new Controls();
            _stateMachine = new StateMachine();
        }
        private void OnEnable() => InputActions.Enable();
        private void OnDisable() => InputActions.Disable();

        private void Start()
        {
            _stateMachine.Initialize(new PlayerIdleState(this));
        }
        private void Update()
        {
            _stateMachine.Tick();
        }
        public void SwitchState(IState newState)
        {
            _stateMachine.ChangeState(newState);
        }
    }
}