using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HG100.Models
{
    public class PLantFilterViewModel
    {
       [Display(Name = "ZONE")]
       public int zone { get; set; }
        [Display(Name ="PLANT TYPE")]
        public string PlantType { get; set; }
        //[Display(Name ="ORDER BY")]
        //public PlantOrderType OrderType { get; set; }

    }
    //public enum PlantOrderType
    //{
    //    [Display(Name = "PLANT TYPE")]
    //    PlantType,
    //    [Display (Name ="COLOR")]
    //    Color
    //}
}