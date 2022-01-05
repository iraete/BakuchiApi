﻿using System;
using BakuchiApi.Models.Enums;

namespace BakuchiApi.Contracts.Requests
{
    public class CreatePoolDto
    {
        public Guid EventId { get; set; }
        public string Alias { get; set; }
        public BetType BetType { get; set; }
        public string Description { get; set; }
    }
}