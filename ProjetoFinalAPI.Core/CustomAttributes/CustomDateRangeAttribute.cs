
using System.ComponentModel.DataAnnotations;


namespace ProjetoFinalAPI.Core.CustomAttributes
{
    internal class CustomDateRangeAttribute : RangeAttribute
    {
        internal CustomDateRangeAttribute() : base(typeof(DateTime), DateTime.Now.ToString(), DateTime.MaxValue.ToString())
        { 
        }
    }
}
