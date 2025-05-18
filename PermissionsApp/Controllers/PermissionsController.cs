using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PermissionsApp.Domain;
using PermissionsApp;
using PermissionsApp.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IKafkaProducer _kafkaProducer;
    private readonly IElasticsearchService _elasticsearchService;

    public PermissionsController(IUnitOfWork unitOfWork, IKafkaProducer kafkaProducer, IElasticsearchService elasticsearchService)
    {
        _unitOfWork = unitOfWork;
        _kafkaProducer = kafkaProducer;
        _elasticsearchService = elasticsearchService;
    }

    [HttpGet("GetPermissionTypes")]
    public async Task<IActionResult> GetPermissionTypes()
    {
        var list = await _unitOfWork.Permissions.GetAllAsync();

        var dtoList = list.Select(p => new PermissionTypeDTO
        {
            Id = p.PermissionType.Id,
            Description = p.PermissionType.Description
           
        }).ToList();

        return Ok(dtoList);
    }

    [HttpGet("GetPermissions")]
    public async Task<IActionResult> GetPermissions()
    {
        var list = await _unitOfWork.Permissions.GetAllAsync();

        var dtoList = list.Select(p => new PermissionDto
        {
            Id = p.Id,
            EmployeeFirstName = p.EmployeeName,
            EmployeeLastName = p.EmployeeLastName,
            PermissionTypeId = p.PermissionTypeId,
            PermissionDate = p.PermissionDate
        }).ToList();

        return Ok(dtoList);
    }

}