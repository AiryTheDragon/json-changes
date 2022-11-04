using System;
using System.Collections.Generic;

namespace NPC
{
    public class BaseAI
    {
        public int Id { get; set; }
        public PersonInfo Self { get; set; }
        protected Dictionary<int, PersonInfo> Relationships { get; set; } = new();
    }
}