using System;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class BarEvent
    {
        /// <summary>
        /// Property for the foreign key to the bars bar name. Has a maximum length of 150 just like in the bar.
        /// </summary>
        [MaxLength(150)]
        public string BarName { get; set; }
        

        /// <summary>
        /// Property for setting/getting the name of the event. Has a max length of 75.
        /// </summary>
        [MaxLength(75)]
        public string EventName { get; set; }


        /// <summary>
        /// Property for the date of the event.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Property for getting and setting the image of a BarEvent
        /// </summary>
        public string Image { get; set; }


        /// <summary>
        /// Navigation property for finding the bar of the event.
        /// </summary>
        public virtual Bar Bar { get; set; }
    }
}