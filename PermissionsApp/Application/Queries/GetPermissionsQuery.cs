using MediatR;
using PermissionsApp.Application.DTO;
using System.Collections.Generic;

public class GetPermissionsQuery : IRequest<List<PermissionDto>>
{
}