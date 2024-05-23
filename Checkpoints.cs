using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] private bool _hasCrossedStartLine;
    [SerializeField] private bool _hasCrossedFinishLine;
    [SerializeField] private int _lapsCompleted = 0;
    [SerializeField] private int _totalLaps = 3;
    [SerializeField] private TextMeshProUGUI _lapsCompletedText;
    [SerializeField] private int _numberOfCheckpoints;
    [SerializeField] private List<GameObject> _checkpoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
       _numberOfCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
       //Debug.Log("There are " + _numberOfCheckpoints + " in this  course.");
       UpdateLapsCompletedText(_lapsCompleted, _totalLaps);
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void UpdateLapsCompletedText(int lapsCompleted, int totalLaps)
    {
        _lapsCompletedText.text = lapsCompleted.ToString() + " / " + totalLaps.ToString() + " Laps";
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Start"))
        {
            if(_hasCrossedStartLine)
            {
                _lapsCompleted+= 1;
                UpdateLapsCompletedText(_lapsCompleted, _totalLaps);
                TurnOnCheckpoints();
            }
            else
            {
                _hasCrossedStartLine = true;
            }
        }
        
        if(other.gameObject.CompareTag("Checkpoint"))
        {
            if(_checkpoints.Count < _numberOfCheckpoints)
            {
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                _checkpoints.Add(other.gameObject);
            }
            
        }
    }

    private void TurnOnCheckpoints()
    {
        for(int i = 0; i < _checkpoints.Count; i++)
        {
            //Debug.Log("I enabled " + i + " colliders");
            _checkpoints[i].gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        _checkpoints.Clear();
    }
}
