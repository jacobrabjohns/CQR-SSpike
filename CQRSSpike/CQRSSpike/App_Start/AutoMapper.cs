using AutoMapper;
using CQRSSpike.Models;

namespace CQRSSpike
{
    public class AutoMapperInit
    {
        public static void Initialise()
        {
            Mapper.CreateMap<Customer, CustomerBankBalance>()
                .ForMember(dto => dto.Balance, conf => conf.MapFrom(ol => ol.Account.Balance.Amount))
                .ForMember(dto => dto.Name, conf => conf.MapFrom(cust => cust.FirstName + " " + cust.LastName))
                .ForMember(dto => dto.AccountNumber, conf => conf.MapFrom(cust => cust.Account.AccountNumber.ToString()));
        }
    }
}