﻿namespace Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
