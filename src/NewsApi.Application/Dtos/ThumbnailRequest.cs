using System;
using Newtonsoft.Json;

namespace NewsApi.Application.Dtos
{
    public enum ThumbnailRequestType
    {
        Create = 1,
        Update,
        Delete
    }
    public class ThumbnailRequest
    {
        [JsonIgnore]
        public Guid NewsId { get; set; }
        [JsonIgnore]
        public long FileLength { get; set; }
        public string FileLocalPath { get; set; }
        public string FileName { get; set; }
    }
}