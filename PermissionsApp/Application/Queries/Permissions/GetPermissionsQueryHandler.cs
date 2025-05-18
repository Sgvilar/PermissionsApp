using MediatR;
using AutoMapper;
using PermissionsApp.Application.DTO;
using PermissionsApp.Application.Interfaces;

public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, List<PermissionDto>>
{
    private readonly IPermissionRepository _repository;
    private readonly IMapper _mapper;
    private readonly IKafkaProducer _kafka;

    public GetPermissionsQueryHandler(IPermissionRepository repository, IMapper mapper, IKafkaProducer kafka)
    {
        _repository = repository;
        _mapper = mapper;
        _kafka = kafka;
    }

    public async Task<List<PermissionDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _repository.GetAllAsync();

        await _kafka.SendMessageAsync("permissions-topic", new KafkaMessageDto
        {
            Id = Guid.NewGuid(),
            NameOperation = "get"
        });

        return _mapper.Map<List<PermissionDto>>(permissions);
    }
}
