﻿using Dapper.Contrib.Extensions;

namespace FluxusApi.Entities
{
    [Table("BankBranch")]
    public class BankBranch
    {
        [ExplicitKey] 
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public string ContactName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
    }
}
