﻿namespace VaraticPrim.Domain.Entity;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime UpdatedOnUtc { get; set; }    
}