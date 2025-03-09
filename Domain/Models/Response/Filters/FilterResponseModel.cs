﻿namespace Domain.Models.Response.Filters
{
    public class FilterResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Description { get; set; }
        public int CharacteristicId { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
    }
}
