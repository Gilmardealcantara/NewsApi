using System;
using System.Collections.Generic;
using System.Linq;
using NewsApi.Domain.Entities;

namespace NewsApi.Domain.Dtos
{
    public class NewsResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? NumComments { get { return Comments?.Count(); } }
        public IEnumerable<Comment> Comments { get; set; }
    }
}