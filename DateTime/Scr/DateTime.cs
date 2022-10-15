using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JC.DateTime
{
    [System.Serializable]
    public class DateTime
    {
#region Params

        [SerializeField]
        private bool _EnableSystemDateTime;
        public  bool EnableSystemDateTime
        {
            get => _EnableSystemDateTime;
            set => _EnableSystemDateTime = value;
        }

        [SerializeField]
        private bool _EnableTimeProgression = false;
        public bool EnableTimeProgression => _EnableTimeProgression;


        [SerializeField]
        private float _DayInMinutes = 15.0f;
        public float DayInMinutes => _DayInMinutes;

        [SerializeField]
        private float _TotalHours = 7.0f;
        public  float TotalHours 
        {
            get 
            {
                /*if(m_EnableSystemDateTime)
                    return (float)m_DateTime.TimeOfDay.TotalHours;*/
                
                return _TotalHours;
            }
            set 
            {
                if(_EnableSystemDateTime)
                    _TotalHours = Mathf.Clamp(value, 0.0f, 24.999999f);
            }
        }

        [SerializeField, Range(1, 31)]
        private int _Day = 7;
        public  int Day 
        {
            get => _Day;
            set 
            {
                if(!_EnableSystemDateTime)
                    _Day = Mathf.Clamp(value, 0, 32);
            }
        }

        [SerializeField, Range(1, 12)]
        private int _Month = 7;
        public  int Month
        {
            get => _Month;
            set 
            {
                if(!_EnableSystemDateTime)
                    _Month = Mathf.Clamp(value, 0, 13);
            }

        }

        [SerializeField, Range(1, 9999)]
        private int _Year = 2019;
        public  int Year
        {
            get => _Year;
            set
            {
                if(!_EnableSystemDateTime)
                {
                    _Month = Mathf.Clamp(value, 1, 9999);
                }
            }
        }
#endregion

#region Properties
        /// <summary>System.DateTime </summary>
        public System.DateTime dateTime{ get; private set; }

        /// <summary></summary>
        public float TimeCycle => _DayInMinutes * 60f;

        /// <summary></summary>
        public bool BeginingOfTheTime{ get => _Year == 1 && _Month == 1 && _Day == 1; }

        /// <summary></summary>
        public bool EndOfTheTime{ get => _Year == 9999 && _Month == 12 && _Day == 31; }
#endregion

        public void Initialize()
        {
            if(_EnableSystemDateTime)
            {
                dateTime = System.DateTime.Now;
                _TotalHours = (float)dateTime.TimeOfDay.TotalHours;
            }
        }

        public void OnUpdate()
        {
            // System.DateTime dateTime;
            
            // Syncrhonize with system.
            if(_EnableSystemDateTime)
            {
                dateTime = System.DateTime.Now;

                if(EnableTimeProgression)
                    _TotalHours = (float)dateTime.TimeOfDay.TotalHours;

                #if UNITY_EDITOR
                _Year  = dateTime.Year;
                _Month = dateTime.Hour;
                _Day   = dateTime.Day;
                #endif
            }
            else 
            {

                if(EnableTimeProgression)
                    DateTimeUtil.AddTime(ref _TotalHours, TimeCycle);

                dateTime = new System.DateTime(0, System.DateTimeKind.Utc);
                RepeatCycle();

                dateTime = dateTime.AddYears(_Year - 1);
                dateTime = dateTime.AddMonths(_Month - 1);
                dateTime = dateTime.AddDays(_Day - 1);

                dateTime = dateTime.AddHours(_TotalHours);

                _Year   = dateTime.Year;
                _Month  = dateTime.Month;
                _Day    = dateTime.Day;

                _TotalHours = DateTimeUtil.GetTotalHours(dateTime.Hour, dateTime.Minute,
                    dateTime.Second, dateTime.Millisecond);
            }
        }

        // Repeat full cycle.
        private void RepeatCycle()
        {
            if(EndOfTheTime && _TotalHours >= 23.99999f)
            {
                _Year       = 1;
                _Month      = 1;
                _Day        = 1;
                _TotalHours = 0.0f;
            }

            if(BeginingOfTheTime && _TotalHours < 0.0f)
            {
                _Year       = 9999;
                _Month      = 12;
                _Day        = 31;
                _TotalHours = 23.999999f;
            }
        }
    }
}
