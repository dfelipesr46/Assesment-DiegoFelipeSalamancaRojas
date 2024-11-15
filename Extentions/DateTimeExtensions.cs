using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assesment_DiegoFelipeSalamancaRojas.Extentions
{
    public static class DateTimeExtensions
{
    // Método para obtener el primer día de la semana
    public static DateTime StartOfWeek(this DateTime dateTime)
    {
        var diff = dateTime.DayOfWeek - DayOfWeek.Sunday;
        if (diff < 0)
            diff += 7;

        return dateTime.AddDays(-diff).Date;
    }

    // Método para obtener el último día de la semana
    public static DateTime EndOfWeek(this DateTime dateTime)
    {
        var diff = DayOfWeek.Saturday - dateTime.DayOfWeek;
        if (diff < 0)
            diff += 7;

        return dateTime.AddDays(diff).Date.AddDays(1).AddMilliseconds(-1);
    }
}

}