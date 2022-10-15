using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace JC.DateTime
{
    public static class DateTimeUtil
    {
        public const uint k_HoursPerDay = 24;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetTotalHours(int hour) => (float)hour;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetTotalHours(int hour, int minute)
        {
            return (float)hour + ((float)(minute) / 60.0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetTotalHours(int hour, int minute, int second)
        {
            return (float)hour + ((float)minute / 60f) + ((float)second / 3600f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetTotalHours(int hour, int minute, int second, int millisecond)
        {
            return (float)hour + (float)minute / 60f + (float)second / 3600f + (float)millisecond / 3600000f;
        }

        public static void AddTime(ref float target, float timeCycle)
        {
            if(!Application.isPlaying) return;
            target += (timeCycle != 0) ? Time.deltaTime / timeCycle *
                k_HoursPerDay : 0.0f;
        }
    }
}
