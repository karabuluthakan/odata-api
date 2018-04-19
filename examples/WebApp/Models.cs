﻿using System;
using Mocoding.AspNetCore.ODataApi.DataAccess;
using Mocoding.AspNetCore.ODataApi.EasyDocDb.Helpers;

namespace WebApp
{
    [ReadOptimized]
    public class User : IEntity
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string State { get; set; }
    }

    [ReadOptimized]
    public class Role : IEntity
    {
        public Guid? Id { get; set; }

        public string RoleName { get; set; }
    }

    [ReadOptimized]
    public class KeyValuePair : IEntity
    {
        public Guid? Id { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }
    }

    [ReadOptimized]
    public class Order : IEntity
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }

        public decimal Price { get; set; }
        
    }
}