using AutoMapper;
using VaraticPrim.Framework.Models.ServiceModels;
using VaraticPrim.Repository.Repository.Interfaces;

namespace VaraticPrim.Framework.Managers;

public class ServiceManager
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IMapper _mapper;
    
    public ServiceManager(IServiceRepository serviceRepository, IMapper mapper)
    {
        _serviceRepository = serviceRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ServiceModel>> GetAll()
    {
        var services = await _serviceRepository.GetAll();
        var serviceModels = _mapper.Map<IEnumerable<ServiceModel>>(services);

        return serviceModels;
    }
    
    public async Task<ServiceModel> GetById(int id)
    {
        var service = await _serviceRepository.GetById(id);
        var serviceModel = _mapper.Map<ServiceModel>(service);
        
        return serviceModel;
    }
}