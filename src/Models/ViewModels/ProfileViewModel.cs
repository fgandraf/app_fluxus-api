﻿using Dapper.Contrib.Extensions;

namespace FluxusApi.Models.ViewModels;

[Table("Profile")]
public class ProfileViewModel
{
    public long Id { get; set; }
    public string Cnpj { get; set; }
    public string TradingName { get; set; }
    public string CompanyName { get; set; }
    public string StateId { get; set; }
    public string CityId { get; set; }
    public string Address { get; set; }
    public string Complement { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string Zip { get; set; }
    public string State { get; set; }
    public DateTime EstablishmentDate { get; set; }
    public string Phone1 { get; set; }
    public string Phone2 { get; set; }
    public string Email { get; set; }
    public string BankAccountName { get; set; }
    public string BankAccountType { get; set; }
    public string BankAccountBranch { get; set; }
    public string BankAccountDigit { get; set; }
    public string BankAccountNumber { get; set; }
    public string ContractorName { get; set; }
    public string ContractNotice { get; set; }
    public string ContractNumber { get; set; }
    public DateTime ContractEstablished { get; set; }
    public DateTime ContractStart { get; set; }
    public DateTime ContractEnd { get; set; }
}