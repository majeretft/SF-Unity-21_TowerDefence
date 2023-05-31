using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public enum AiBehaviourEnum
    {
        None,
        Patrol,
        PatrolPointToPoint,
        PatrolSphericalPerimeter,
    }

    [RequireComponent(typeof(Spaceship))]
    public class AiController : MonoBehaviour
    {
        [SerializeField]
        private AiBehaviourEnum _behaviour;
        public AiBehaviourEnum Behaviour { get => _behaviour; set => _behaviour = value; }

        [SerializeField]
        private AIPointPatrol _patrolPoint;
        public AIPointPatrol PatrolPoint { get => _patrolPoint; set => _patrolPoint = value; }

        private List<Vector3> _patrolPoints;
        private int _patrolPointCurrentNumber;

        [Range(0f, 1f)]
        [SerializeField]
        private float _patrolWidthCoef;

        [Range(0f, 90f)]
        [SerializeField]
        private float _patrolTangage;

        [Range(0f, 1f)]
        [SerializeField]
        private float _linearSpeed;

        [Range(0f, 1f)]
        [SerializeField]
        private float _angularSpeed;

        [SerializeField]
        private float _newMoveTargetTimout;

        [SerializeField]
        private float _selectNewTargetTimeout;

        [SerializeField]
        private float _shootTimeout;

        [SerializeField]
        private float _evadeRayLength;

        private Spaceship _ship;

        private Vector3 _moveTarget;

        private Distructible _attackTarget;

        private const float MAX_TORQUE_ANGLE = 45;

        private Timer _randomizeTargetPositionTimer;
        private Timer _fireTimer;
        private Timer _findNewFireTargetTimer;

        private void Start()
        {
            _ship = GetComponent<Spaceship>();

            InitTimers();
        }

        private void Update()
        {
            TickTimers();
            UpdateAi();
        }

        private void UpdateAi()
        {
            if (_behaviour == AiBehaviourEnum.None)
                return;

            if (_behaviour == AiBehaviourEnum.Patrol)
                UpdateBehaviourPatrol();

            if (_behaviour == AiBehaviourEnum.PatrolPointToPoint)
                UpdateBehaviourPatrolP2P();

            if (_behaviour == AiBehaviourEnum.PatrolSphericalPerimeter)
                UpdateBehaviourPatrolPerimeter();
        }

        private void UpdateBehaviourPatrolPerimeter()
        {
            UpdateBehaviourPatrol();
        }

        private void UpdateBehaviourPatrolP2P()
        {
            UpdateBehaviourPatrol();
        }

        private void UpdateBehaviourPatrol()
        {
            ActionFindNewMoveTarget();
            ActionOperateShip();
            ActionFindNewAttackTarget(_patrolPoint.SearchAttackTargetRadius, _patrolPoint.transform.position);
            ActionFire();
            ActionEvadeCollision();
        }

        private void ActionFindNewMoveTarget()
        {
            if (_attackTarget != null)
            {
                _moveTarget = _attackTarget.transform.position;
                return;
            }

            if (_patrolPoint == null)
                return;

            bool isInsidePatrolZone =
                    (_patrolPoint.transform.position - transform.position).sqrMagnitude < _patrolPoint.Radius * _patrolPoint.Radius;

            if (!isInsidePatrolZone)
            {
                _moveTarget = _patrolPoint.transform.position;
                return;
            }

            if (!_randomizeTargetPositionTimer.IsFinished)
                return;

            _randomizeTargetPositionTimer.Restart();

            if (_behaviour == AiBehaviourEnum.Patrol)
            {
                var newPoint = Random.onUnitSphere * _patrolPoint.Radius + _patrolPoint.transform.position;
                _moveTarget = newPoint;
            }
            if (_behaviour == AiBehaviourEnum.PatrolPointToPoint)
            {

                var capacity = 2;
                if (_patrolPoints == null || _patrolPoints.Capacity != capacity)
                {
                    _patrolPoints = new List<Vector3>(capacity);

                    // Создать врещение в начале координат по часовой стрелке
                    var rotation = Quaternion.AngleAxis(-_patrolTangage, Vector3.forward);
                    // Повернуть единичный вектор в начале координат
                    var pointUnitPosition = rotation * Vector3.right;
                    // Установить вектору требуемую длину
                    var pointPosition = pointUnitPosition * _patrolWidthCoef * _patrolPoint.Radius;
                    // Переместить вектор из начала координат в круг патруля
                    var point1 = pointPosition + _patrolPoint.transform.position;
                    // Создать второй вектор, который направлен в противоположную сторону
                    var point2 = pointPosition * -1 + _patrolPoint.transform.position;

                    _patrolPoints.Add(point1);
                    _patrolPoints.Add(point2);
                }

                if (_moveTarget == _patrolPoints[0])
                    _moveTarget = _patrolPoints[1];
                else
                    _moveTarget = _patrolPoints[0];
            }

            if (_behaviour == AiBehaviourEnum.PatrolSphericalPerimeter)
            {
                var patrolPointsAmount = Mathf.FloorToInt(2 * Mathf.PI * _patrolPoint.Radius / (_patrolPoint.Radius * _patrolWidthCoef));
                if (patrolPointsAmount < 3)
                    patrolPointsAmount = 3;
                if (_patrolPointCurrentNumber < 1)
                    _patrolPointCurrentNumber = 1;
                if (_patrolPointCurrentNumber > patrolPointsAmount)
                    _patrolPointCurrentNumber = 1;

                var rotation = Quaternion.AngleAxis(-360 * _patrolPointCurrentNumber / patrolPointsAmount, Vector3.forward);

                var dirUnit = rotation * Vector3.up;

                var currentPatrolTarget = dirUnit * _patrolPoint.Radius * _patrolWidthCoef + _patrolPoint.transform.position;
                _moveTarget = currentPatrolTarget;
                _patrolPointCurrentNumber++;


                // var capacity = Mathf.FloorToInt(_patrolPoint.Radius / Mathf.PI * 6);
                // if (_patrolPoints == null || _patrolPoints.Capacity != capacity)
                // {
                //     _patrolPoints = new List<Vector3>(capacity);


                // }

                // for (int i = 0; i < _patrolPoints.Count; i++)
                // {
                //     if (_moveTarget == _patrolPoints[i])
                //     {
                //         if (i == _patrolPoints.Count - 1)
                //             _moveTarget = _patrolPoints[0];
                //         else
                //             _moveTarget = _patrolPoints[i + 1];

                //         break;
                //     }
                // }
            }
        }

        private void ActionOperateShip()
        {
            _ship.ThrustControl = _linearSpeed;

            _ship.TorqueControl =
                AiController.ComputeAlignTorqueNormalized(_moveTarget, _ship.transform) * _angularSpeed;
        }

        private static float ComputeAlignTorqueNormalized(Vector3 targerPosition, Transform shipTransform)
        {
            var localTargetPosition = shipTransform.InverseTransformPoint(targerPosition);

            var angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);
            angle = Mathf.Clamp(angle, -MAX_TORQUE_ANGLE, MAX_TORQUE_ANGLE) / MAX_TORQUE_ANGLE;

            return -angle;
        }

        private void ActionFindNewAttackTarget(float maxDistance, Vector2 searchOrigin)
        {
            if (!_findNewFireTargetTimer.IsFinished)
                return;

            _findNewFireTargetTimer.Restart();

            _attackTarget = FindClosestDistructible(maxDistance, searchOrigin);
        }

        private Distructible FindClosestDistructible(float maxDistance, Vector2 searchOrigin)
        {
            var maxDist = maxDistance;
            Distructible closest = null;

            foreach (var d in Distructible.AllDistructibles)
            {
                if (d.TeamId == Distructible.TEAM_NEUTRAL_ID)
                    continue;

                if (d.TeamId == _ship.TeamId)
                    continue;

                if (d.GetComponent<Spaceship>() == _ship)
                    continue;

                var dist = Vector2.Distance(searchOrigin, d.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    closest = d;
                }
            }

            return closest;
        }

        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, _evadeRayLength))
            {
                _moveTarget = transform.position + transform.right * 100;
            }
        }

        private void ActionFire()
        {
            if (!_attackTarget)
                return;

            if (!_fireTimer.IsFinished)
                return;

            var angle = Vector2.Angle(_ship.transform.up, (_attackTarget.transform.position - _ship.transform.position).normalized);
            if (angle < 30)
            {
                _ship.Fire(TurretModeEnum.Primary);
                _fireTimer.Restart();
            }
        }

        #region Timers

        private void InitTimers()
        {
            _randomizeTargetPositionTimer = new Timer(_newMoveTargetTimout);
            _fireTimer = new Timer(_shootTimeout);
            _findNewFireTargetTimer = new Timer(_selectNewTargetTimeout);
        }

        private void TickTimers()
        {
            _randomizeTargetPositionTimer.Tick(Time.deltaTime);
            _fireTimer.Tick(Time.deltaTime);
            _findNewFireTargetTimer.Tick(Time.deltaTime);
        }

        private void SetPatrolBehaviour(AIPointPatrol patrolPoint)
        {
            _behaviour = AiBehaviourEnum.Patrol;
            _patrolPoint = patrolPoint;
        }

        #endregion

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0);
            Gizmos.DrawSphere(_moveTarget, 0.1f);
            Gizmos.DrawLine(transform.position, transform.position + transform.up * _evadeRayLength);

            if (_patrolPoints != null)
                foreach (var p in _patrolPoints)
                    Gizmos.DrawLine(_patrolPoint.transform.position, p);
        }
    }
}
