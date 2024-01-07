using System.Collections;
using System.Globalization;
using FluxusApi.Models.DTO;
using FluxusApi.Models.Enums;
using FluxusApi.Repositories.Contracts;

namespace FluxusApi.Repositories.Mock;

public class ServiceOrderRepositoryMock : RepositoryMock<ServiceOrderDTO>, IServiceOrderRepository
{
    public ServiceOrderRepositoryMock()
        => InitializeData();
    
    
    public Task<IEnumerable> GetOrdersFlowAsync()
    {
        var serviceOrderRepository = Repository;
        var serviceRepository = new ServiceRepositoryMock().GetAllAsync().Result;
        
        var result = serviceOrderRepository
            .Where(
                order => order.InvoiceId == 0)
            .Join(serviceRepository,
                order => order.ServiceId,
                service => service.Id,
                (order, service) => 
                {
                    var firstPart = order.ReferenceCode.Split('/')[0];
                    var numberPart = firstPart.Split('.').LastOrDefault();
                    var extractedNumber = int.TryParse(numberPart, out int number) ? number.ToString() : "0";

                    return new
                    {
                        Id = order.Id,
                        Title = $"{service.Tag}-{order.City}-{extractedNumber}\n\n{order.CustomerName.Replace(" ", " ")}\n- Prazo: {order.Deadline.ToString("dd/MM/yyyy")}",
                        Status = order.Status,
                        ProfessionalId = order.ProfessionalId,
                        OrderDate = order.OrderDate
                    };
                })
            .OrderBy(order => order.OrderDate)
            .ToList();
        

        return Task.FromResult<IEnumerable>(result);
    }

    public Task<IEnumerable> GetInvoicedAsync(int invoiceId)
    {
        var professionalsRepository = new ProfessionalRepositoryMock().GetAllAsync().Result;
        var serviceRepository = new ServiceRepositoryMock().GetAllAsync().Result;
        var serviceOrderRepository = Repository;

        var result = serviceOrderRepository
            .Where(
                order => order.InvoiceId == invoiceId)
            .Join(
                serviceRepository,
                order => order.ServiceId,
                service => service.Id,
                (order, service) => new { order, service })
            .Join(professionalsRepository,
                orderService => orderService.order.ProfessionalId,
                professional => professional.Id,
                (orderService, professional) => new
                {
                    Id = orderService.order.Id,
                    OrderDate = orderService.order.OrderDate,
                    ReferenceCode = orderService.order.ReferenceCode,
                    ProfessionalId = orderService.order.ProfessionalId,
                    Professional = professional.Tag,
                    Service = orderService.service.Tag,
                    City = orderService.order.City,
                    CustomerName = orderService.order.CustomerName,
                    SurveyDate = orderService.order.SurveyDate,
                    DoneDate = orderService.order.DoneDate,
                    InvoiceId = orderService.order.InvoiceId,
                    Status = orderService.order.Status,
                    ServiceAmount = orderService.order.ServiceAmount,
                    MileageAllowance = orderService.order.MileageAllowance
                })
            .Distinct()
            .OrderBy(x => x.DoneDate)
            .ToList();

        return Task.FromResult<IEnumerable>(result);
    }

    public Task<IEnumerable> GetDoneToInvoiceAsync()
    {
        var professionalsRepository = new ProfessionalRepositoryMock().GetAllAsync().Result;
        var serviceRepository = new ServiceRepositoryMock().GetAllAsync().Result;
        var serviceOrderRepository = Repository;

        var result = serviceOrderRepository
            .Where(
                order => order.Invoiced == false &&
                         order.Status == EnumStatus.CONCLUIDA)
            .Join(
                serviceRepository,
                order => order.ServiceId,
                service => service.Id,
                (order, service) => new { order, service })
            .Join(professionalsRepository,
                orderService => orderService.order.ProfessionalId,
                professional => professional.Id,
                (orderService, professional) => new
                {
                    Id = orderService.order.Id,
                    OrderDate = orderService.order.OrderDate,
                    ReferenceCode = orderService.order.ReferenceCode,
                    Professional = professional.Tag,
                    Service = orderService.service.Tag,
                    City = orderService.order.City,
                    CustomerName = orderService.order.CustomerName,
                    SurveyDate = orderService.order.SurveyDate,
                    DoneDate = orderService.order.DoneDate,
                    ServiceAmount = orderService.order.ServiceAmount,
                    MileageAllowance = orderService.order.MileageAllowance
                })
            .Distinct()
            .OrderBy(x => x.DoneDate)
            .ToList();

        return Task.FromResult<IEnumerable>(result);
    }

    public Task<IEnumerable> GetFilteredAsync(string filter)
    {
        var professionalsRepository = new ProfessionalRepositoryMock().GetAllAsync().Result;
        var serviceRepository = new ServiceRepositoryMock().GetAllAsync().Result;
        var serviceOrderRepository = Repository;
        
        var filters = filter.Split(',');
        var param = new
        {
            professional = filters[0],
            service = filters[1],
            city = filters[2],
            status = filters[3],
            invoiced = Convert.ToBoolean(filters[4])
        };
        
        var result = serviceOrderRepository
            .Join(
                serviceRepository,
                order => order.ServiceId,
                service => service.Id,
                (order, service) => new { order, service })
            .Join(
                professionalsRepository,
                orderService => orderService.order.ProfessionalId,
                professional => professional.Id,
                (orderService, professional) => new
                {
                    Order = orderService.order,
                    Service = orderService.service,
                    Professional = professional
                })
            .Where(x => x.Order.Invoiced == param.invoiced &&
                        x.Order.Status.ToString().Contains(param.status) &&
                        x.Order.City.Contains(param.city) &&
                        x.Professional.Tag.Contains(param.professional) &&
                        x.Service.Tag.Contains(param.service))
            .Select(x => new
            {
                Id = x.Order.Id,
                Status = x.Order.Status,
                Professional = x.Professional.Tag,
                OrderDate = x.Order.OrderDate,
                ReferenceCode = x.Order.ReferenceCode,
                Service = x.Service.Tag,
                City = x.Order.City,
                CustomerName = x.Order.CustomerName,
                SurveyDate = x.Order.SurveyDate,
                DoneDate = x.Order.DoneDate,
                Invoiced = x.Order.Invoiced
            })
            .OrderBy(x => x.DoneDate)
            .ToList();

        return Task.FromResult<IEnumerable>(result);
    }
    
    
    public Task<IEnumerable> GetProfessionalAsync(int invoiceId)
    {
        var professionalsRepository = new ProfessionalRepositoryMock().GetAllAsync().Result;
        var serviceOrderRepository = Repository;
        
        var result = serviceOrderRepository
            .Where(
                order => order.InvoiceId == invoiceId)
            .Join(
                professionalsRepository,
                order => order.ProfessionalId,
                professional => professional.Id,
                (order, professional) => new
                {
                    ProfessionalId = professional.Id,
                    Nameid = (professional.Profession != null ? professional.Profession.Substring(0, Math.Min(3, professional.Profession.Length)) : "") 
                             + ". " 
                             + professional.Name.Split(' ')[0] 
                             + " " 
                             + (professional.Name.Split(' ').Length > 1 ? professional.Name.Split(' ')[1] : "")
                }
            )
            .Distinct()
            .OrderBy(x => x.Nameid);
        
        return Task.FromResult<IEnumerable>(result);
    }

    public Task<IEnumerable> GetOrderedCitiesAsync()
    {
        var distinctCities = Repository
            .GroupBy(g => g.City)
            .OrderBy(o => o.Key)
            .Select(s => new { City = s.Key })
            .ToList();

        return Task.FromResult<IEnumerable>(distinctCities);
    }

    public Task<int> UpdateInvoiceIdAsync(int invoiceId, List<int> orders)
    {
        bool invoiced = invoiceId > 0;
        
        foreach (var item in orders)
        {
            var serviceOrder = Repository.SingleOrDefault(x => x.Id == item )!;
            
            serviceOrder.InvoiceId = invoiceId;
            serviceOrder.Invoiced = true;
        }

        return Task.FromResult(1);
    }

    public Task<int> UpdateStatusAsync(int id, EnumStatus status)
    {
        var serviceOrder = Repository.SingleOrDefault(x => x.Id == id )!;
        serviceOrder.Status = status;
        
        switch (status)
        {
            case EnumStatus.RECEBIDA: break;
            case EnumStatus.PENDENTE: serviceOrder.PendingDate = DateTime.Now.ToString(CultureInfo.CurrentCulture); break;
            case EnumStatus.VISTORIADA: serviceOrder.SurveyDate = DateTime.Now.ToString(CultureInfo.CurrentCulture); break;
            case EnumStatus.CONCLUIDA: serviceOrder.DoneDate = DateTime.Now.ToString(CultureInfo.CurrentCulture); break;
            default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
        }

        return Task.FromResult(id);
    }

    private void InitializeData()
    {
        if (Repository.Count == 0)
        {
            Repository = new List<ServiceOrderDTO>
            {
                new ServiceOrderDTO { Id = 1, ReferenceCode = "9999.0287.000320870/2023.01.01.01", Branch = "0287", OrderDate = "06/08/2023 00:00:00", Deadline = DateTime.Parse("2023-06-15T00:00:00"), ProfessionalId = 1, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "ZILDA DE FATIMA FERREIRA", City = "BARIRI", ContactName = "NILSON", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.VISTORIADA, PendingDate = null, SurveyDate = "06/10/2023 00:00:00", DoneDate = null, Invoiced = false, InvoiceId = 0 },
                new ServiceOrderDTO { Id = 2, ReferenceCode = "9999.0287.000355982/2023.01.01.01", Branch = "0287", OrderDate = "06/23/2023 00:00:00", Deadline = DateTime.Parse("2023-06-29T00:00:00"), ProfessionalId = 1, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "ANTONIO CARLOS BUDIN", City = "BARIRI", ContactName = "ANTONIO", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.PENDENTE, PendingDate = "06/25/2023 00:00:00", SurveyDate = null, DoneDate = null, Invoiced = false, InvoiceId = 1 },
                new ServiceOrderDTO { Id = 3, ReferenceCode = "9999.0287.000394850/2023.01.01.01", Branch = "0287", OrderDate = "07/09/2023 00:00:00", Deadline = DateTime.Parse("2023-07-15T00:00:00"), ProfessionalId = 1, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "JOÃO PAULO BARBIERI SILVA", City = "BARIRI", ContactName = "LEANDRO", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.RECEBIDA, PendingDate = null, SurveyDate = null, DoneDate = null, Invoiced = false, InvoiceId = 0 },
                new ServiceOrderDTO { Id = 4, ReferenceCode = "9999.0287.000405602/2023.01.01.01", Branch = "0287", OrderDate = "07/14/2023 00:00:00", Deadline = DateTime.Parse("2023-07-20T00:00:00"), ProfessionalId = 1, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "LUCAS WILLIAN DE FREITAS", City = "ITAPUÍ", ContactName = "PAULINHO", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.CONCLUIDA, PendingDate = null, SurveyDate = "07/16/2023 00:00:00", DoneDate = "07/17/2023 00:00:00", Invoiced = false, InvoiceId = 0 },
                new ServiceOrderDTO { Id = 5, ReferenceCode = "9999.0287.000430482/2023.01.01.01", Branch = "0287", OrderDate = "07/23/2023 00:00:00", Deadline = DateTime.Parse("2023-07-29T00:00:00"), ProfessionalId = 1, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "JOSE CAMPOS DE CARVALHO", City = "BARIRI", ContactName = "CLEBER", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.CONCLUIDA, PendingDate = null, SurveyDate = "07/28/2023 00:00:00", DoneDate = "07/28/2023 00:00:00", Invoiced = true, InvoiceId = 1 },
                new ServiceOrderDTO { Id = 6, ReferenceCode = "9999.0290.000443357/2023.01.01.01", Branch = "0290", OrderDate = "07/29/2023 00:00:00", Deadline = DateTime.Parse("2023-08-04T00:00:00"), ProfessionalId = 1, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "TABATA HELENA ROQUE", City = "BAURU", ContactName = "SAULO", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.CONCLUIDA, PendingDate = null, SurveyDate = "07/30/2023 00:00:00", DoneDate = "07/30/2023 00:00:00", Invoiced = true, InvoiceId = 2 },
                new ServiceOrderDTO { Id = 7, ReferenceCode = "9999.0315.000324014/2023.01.01.01", Branch = "0315", OrderDate = "06/09/2023 00:00:00", Deadline = DateTime.Parse("2023-06-16T00:00:00"), ProfessionalId = 1, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "CAUE GATTO VIRGILIO", City = "JAÚ", ContactName = "GABRIEL", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.CONCLUIDA, PendingDate = null, SurveyDate = "06/11/2023 00:00:00", DoneDate = "06/11/2023 00:00:00", Invoiced = true, InvoiceId = 1 },
                new ServiceOrderDTO { Id = 8, ReferenceCode = "9999.0318.000427108/2023.01.01.01", Branch = "0318", OrderDate = "07/22/2023 00:00:00", Deadline = DateTime.Parse("2023-07-28T00:00:00"), ProfessionalId = 1, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "LEANDRO ELIAS DOS SANTOS", City = "LINS", ContactName = "LEANDRO", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.CONCLUIDA, PendingDate = null, SurveyDate = "07/27/2023 00:00:00", DoneDate = "07/27/2023 00:00:00", Invoiced = true, InvoiceId = 1 },
                new ServiceOrderDTO { Id = 9, ReferenceCode = "9999.0962.000382818/2023.01.01.01", Branch = "0962", OrderDate = "07/03/2023 00:00:00", Deadline = DateTime.Parse("2023-07-09T00:00:00"), ProfessionalId = 1, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "GUILHERME DE OLIVEIRA RAPHAELI", City = "L. PAULISTA", ContactName = "LUIZ GUILHERME", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.CONCLUIDA, PendingDate = null, SurveyDate = "07/06/2023 00:00:00", DoneDate = "07/07/2023 00:00:00", Invoiced = true, InvoiceId = 1 },
                new ServiceOrderDTO { Id = 10, ReferenceCode = "9999.1920.000342453/2023.01.01.01", Branch = "1920", OrderDate = "06/17/2023 00:00:00", Deadline = DateTime.Parse("2023-06-23T00:00:00"), ProfessionalId = 1, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "MIRIAN DUTRA GUERREIRO", City = "MARÍLIA", ContactName = "JOÃO", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.CONCLUIDA, PendingDate = null, SurveyDate = "06/22/2023 00:00:00", DoneDate = "06/22/2023 00:00:00", Invoiced = true, InvoiceId = 1 },
                new ServiceOrderDTO { Id = 11, ReferenceCode = "9999.1920.000408773/2023.01.01.01", Branch = "1920", OrderDate = "07/15/2023 00:00:00", Deadline = DateTime.Parse("2023-07-21T00:00:00"), ProfessionalId = 1, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "JEFERSON CANDIDO DE OLIVEIRA", City = "JÚLIO MESQUITA", ContactName = "PAULO HENRIQUE", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.CONCLUIDA, PendingDate = null, SurveyDate = "07/20/2023 00:00:00", DoneDate = "07/20/2023 00:00:00", Invoiced = false, InvoiceId = 0 },
                new ServiceOrderDTO { Id = 12, ReferenceCode = "9999.1996.000388357/2023.01.01.01", Branch = "1996", OrderDate = "07/07/2023 00:00:00", Deadline = DateTime.Parse("2023-07-13T00:00:00"), ProfessionalId = 1, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "KLEBER NUNES DOS SANTOS", City = "BAURU", ContactName = "KLEBER", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.CONCLUIDA, PendingDate = null, SurveyDate = "07/06/2023 00:00:00", DoneDate = "07/07/2023 00:00:00", Invoiced = true, InvoiceId = 2 },
                new ServiceOrderDTO { Id = 13, ReferenceCode = "9999.1996.000415684/2023.01.01.01", Branch = "1996", OrderDate = "07/17/2023 00:00:00", Deadline = DateTime.Parse("2023-07-23T00:00:00"), ProfessionalId = 1, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "VANESSA APARECIDA DE LIMA", City = "REGINÓPOLIS", ContactName = "RAFAEL LIMA", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.PENDENTE, PendingDate = "11/17/2022 00:00:00", SurveyDate = null, DoneDate = null, Invoiced = false, InvoiceId = 0 },
                new ServiceOrderDTO { Id = 14, ReferenceCode = "9999.1996.000423005/2023.01.01.01", Branch = "1996", OrderDate = "07/21/2023 00:00:00", Deadline = DateTime.Parse("2023-07-27T00:00:00"), ProfessionalId = 1, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "FABRICIO JULIAO MIGUEL", City = "DUARTINA", ContactName = "FABRICIO REIS", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.CONCLUIDA, PendingDate = null, SurveyDate = "07/21/2023 00:00:00", DoneDate = "07/22/2023 00:00:00", Invoiced = true, InvoiceId = 1 },
                new ServiceOrderDTO { Id = 15, ReferenceCode = "9999.2032.000344975/2023.01.01.01", Branch = "2032", OrderDate = "06/18/2023 00:00:00", Deadline = DateTime.Parse("2023-06-24T00:00:00"), ProfessionalId = 1, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "JAQUELINE MARCELO DA CUNHA", City = "JAÚ", ContactName = "JOSÉ AUGUSTO", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.CONCLUIDA, PendingDate = null, SurveyDate = "06/23/2023 00:00:00", DoneDate = "06/23/2023 00:00:00", Invoiced = true, InvoiceId = 1 },
                new ServiceOrderDTO { Id = 16, ReferenceCode = "9999.2032.000378519/2023.01.01.01", Branch = "2032", OrderDate = "07/02/2023 00:00:00", Deadline = DateTime.Parse("2023-07-08T00:00:00"), ProfessionalId = 2, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "FLAVIO WITT DE OLIVEIRA", City = "JAÚ", ContactName = "MARIA LUIZA", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.RECEBIDA, PendingDate = null, SurveyDate = null, DoneDate = null, Invoiced = false, InvoiceId = 0 },
                new ServiceOrderDTO { Id = 17, ReferenceCode = "9999.2032.000434229/2023.01.01.01", Branch = "2032", OrderDate = "07/24/2023 00:00:00", Deadline = DateTime.Parse("2023-07-30T00:00:00"), ProfessionalId = 2, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "FERNANDO INACIO DOS SANTOS", City = "BOCAINA", ContactName = "TATIANA", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.CONCLUIDA, PendingDate = null, SurveyDate = "07/28/2023 00:00:00", DoneDate = "07/28/2023 00:00:00", Invoiced = false, InvoiceId = 0 },
                new ServiceOrderDTO { Id = 18, ReferenceCode = "9999.2141.000356615/2023.01.01.01", Branch = "2141", OrderDate = "06/23/2023 00:00:00", Deadline = DateTime.Parse("2023-06-29T00:00:00"), ProfessionalId = 2, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "EUZEBIO RUIZ ARRUDA", City = "BAURU", ContactName = "RUBENS", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.CONCLUIDA, PendingDate = null, SurveyDate = "06/29/2023 00:00:00", DoneDate = "06/29/2023 00:00:00", Invoiced = true, InvoiceId = 2 },
                new ServiceOrderDTO { Id = 19, ReferenceCode = "9999.2141.000412634/2023.01.01.01", Branch = "2141", OrderDate = "07/16/2023 00:00:00", Deadline = DateTime.Parse("2023-07-21T00:00:00"), ProfessionalId = 2, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "MIRELY ROSALIN DE MATOS", City = "BAURU", ContactName = "VAGNER LUIS", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.CONCLUIDA, PendingDate = null, SurveyDate = "07/21/2023 00:00:00", DoneDate = "07/21/2023 00:00:00", Invoiced = true, InvoiceId = 2 },
                new ServiceOrderDTO { Id = 20, ReferenceCode = "9999.3254.000323929/2023.01.01.01", Branch = "3254", OrderDate = "06/09/2023 00:00:00", Deadline = DateTime.Parse("2023-06-16T00:00:00"), ProfessionalId = 2, ServiceId = 1, ServiceAmount = 369, MileageAllowance = 37, Siopi = true, CustomerName = "LEONARDO APARECIDO DA SILVA", City = "BAURU", ContactName = "FERNANDO", ContactPhone = "(11) 99999-8888", Coordinates = "S00º00'00.0\" W00º00'00.0\"", Status = EnumStatus.VISTORIADA, PendingDate = null, SurveyDate = "06/15/2023 00:00:00", DoneDate = null, Invoiced = false, InvoiceId = 0 }
            };
        }
    }
}