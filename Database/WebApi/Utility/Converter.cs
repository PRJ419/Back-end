using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;


namespace WebApi.Utility
{
    /// <summary>
    /// Static utility class used to convert Lists of Dtos to their Model equivalents. 
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Static function that converts a IEnumerable&lt;From&gt; to a List&lt;To&gt;
        /// </summary>
        /// <typeparam name="To">
        /// Generic parameter. 
        /// </typeparam>
        /// <typeparam name="From">
        /// Generic parameter. 
        /// </typeparam>
        /// <param name="from">
        /// List that should be converted to the type of To
        /// </param>
        /// <param name="mapper">
        /// IMapper to help the mapping. 
        /// </param>
        /// <returns>
        /// a list of List&lt;To&gt; equivalent of the input param from. 
        /// </returns>
        public static List<To> GenericListConvert<From, To>(IEnumerable<From> from, IMapper mapper)
                where To : class
                where From : class
        {
            var list = new List<To>();
            foreach (var data in from)
            {
                list.Add(mapper.Map<To>(data));
            }

            return list;
        }
    }
}
