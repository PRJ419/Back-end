using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database;

namespace WebApi.DTOs.Bars
{
    public class BarSimpleDto
    {
        [Required]
        public string BarName { get; set; }
        [Range(0.0, 5.0)]
        public double AvgRating { get; set; }
        [MaxLength(500)]
        public string ShortDescription { get; set; }
    }
    ///// <summary>
    ///// Transfer object for the listview of bars
    ///// </summary>
    //public class BarSimpleDto
    //{
    //    /// <summary>
    //    /// Constructor for the Dto.
    //    /// </summary>
    //    /// <param name="name">
    //    /// BarName
    //    /// </param>
    //    /// <param name="avgRating">
    //    /// Average rating
    //    /// </param>
    //    /// <param name="shortDescription">
    //    /// Short description of the bar
    //    /// </param>
    //    public BarSimpleDto(string name, double avgRating, string shortDescription)
    //    {
    //        this.BarName = name;
    //        this.AvgRating = avgRating;
    //        this.ShortDescription = shortDescription;
    //    }

    //    /// <summary>
    //    /// Converts List&lt;Bar&gt; to List&lt;BarSimpleDto&gt;
    //    /// </summary>
    //    /// <param name="list">
    //    /// list param is holding Bars, type List&lt;Bar&gt;
    //    /// </param>
    //    /// <returns>
    //    /// List holding ListViewBarDto versions of the supplied bars. 
    //    /// </returns>
    //    /// <example> var list = BarSimpleDto.FromBarListToDtoList(list)</example>
    //    static public List<BarSimpleDto> FromBarListToDtoList(List<Bar> list)
    //    {
    //            // List to return
    //            var listOfBars = new List<BarSimpleDto>();
    //            // Add all dtos to list
    //            foreach (var bar in list)
    //            {
    //                var barDTO = new BarSimpleDto(bar.BarName,
    //                                                bar.AvgRating,
    //                                                bar.ShortDescription);
    //                listOfBars.Add(barDTO);
    //            }
    //            return listOfBars;
    //    }

    //    // Max length 150
    //    private string _BarName { get; set; }
    //    public string BarName
    //    {
    //        get { return _BarName;}
    //        private set
    //        {
    //            if (value.Length <= 150)
    //                _BarName = value;
    //            else
    //            {
    //                throw new ArgumentOutOfRangeException();
    //            }
    //        } }

    //    // Rating from 0.0 to 5.0
    //    private double _AvgRating { get; set; }

    //    public double AvgRating
    //    {
    //        get { return _AvgRating; }
    //        set
    //        {
    //            if (value >= 0.0 && value <= 5.0)
    //                _AvgRating = value;
    //            else 
    //                throw new ArgumentOutOfRangeException();
    //        }
    //    }

    //    // max length 500
    //    private string _ShortDescription;

    //    public string ShortDescription
    //    {
    //        get { return _ShortDescription; }
    //        set
    //        {
    //            if (value.Length <= 500)
    //                _ShortDescription = value;
    //            else 
    //                throw new ArgumentOutOfRangeException();
    //        }
    //    }
    //}
}