using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [Header("REFERENCES")] 
    [SerializeField] private Rigidbody _rb;
    [HideInInspector] private GameObject _target;
    private Rigidbody _targetRB;
    [SerializeField] private GameObject _explosionPrefab;

    [Header("MOVEMENT")] 
    [SerializeField] private float _speed = 15;
    [SerializeField] private float _rotateSpeed = 95;

    [Header("PREDICTION")] 
    [SerializeField] private float _maxDistancePredict = 100;
    [SerializeField] private float _minDistancePredict = 5;
    [SerializeField] private float _maxTimePrediction = 5;
    private Vector3 _standardPrediction, _deviatedPrediction;

    [Header("DEVIATION")] 
    [SerializeField] private float _deviationAmount = 50;
    [SerializeField] private float _deviationSpeed = 2;

    [Header("LIFE TIME")]
    [SerializeField] private float _maxLifeTime = 10; 

    private void Start()
    {
        if( _target )
        {
            _targetRB = _target.GetComponent<Rigidbody>();
        }
        StartCoroutine(RocketLifeTimer());
    }
    private void FixedUpdate() {
        _rb.velocity = transform.forward * _speed;

        if( _target == null )
        {
            return;
        }
        float leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, Vector3.Distance(transform.position, _target.transform.position));
     
        PredictMovement(leadTimePercentage);

        AddDeviation(leadTimePercentage);

        RotateRocket();
    }

    private void PredictMovement(float leadTimePercentage) {
        var predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);

        _standardPrediction = _targetRB.position + _targetRB.velocity * predictionTime;
    }

    private void AddDeviation(float leadTimePercentage) {
        var deviation = new Vector3(Mathf.Cos(Time.time * _deviationSpeed), 0, 0);
        
        var predictionOffset = transform.TransformDirection(deviation) * _deviationAmount * leadTimePercentage;

        _deviatedPrediction = _standardPrediction + predictionOffset;
    }

    private void RotateRocket() {
        var heading = _deviatedPrediction - transform.position;

        var rotation = Quaternion.LookRotation(heading);
        _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision) {
        if(_explosionPrefab) Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        if (collision.transform.TryGetComponent<IExplode>(out var ex)) ex.Explode();

        Destroy(gameObject);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _standardPrediction);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_standardPrediction, _deviatedPrediction);
    }

    public void SetTarget(GameObject gameObject)
    {
        if( gameObject != null )
        {
            _target = gameObject;
            _targetRB = _targetRB.GetComponent<Rigidbody>();
        }
    }
    IEnumerator RocketLifeTimer()
    {
        yield return new WaitForSeconds(_maxLifeTime);
        Destroy( this.gameObject );
    }
}
