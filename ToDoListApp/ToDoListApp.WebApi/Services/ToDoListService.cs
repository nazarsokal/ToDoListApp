using Microsoft.EntityFrameworkCore;
using ToDoList.WebApi.Models;

namespace ToDoListApp.WebApi.Services;

using AutoMapper;
using ToDoList.Common.Models;
using ToDoList.WebApp.Models.Dto;
using ToDoListApp.Database;
using ToDoListApp.WebApi.Mapper;
using ToDoListApp.WebApi.Services.ServiceContracts;

public class ToDoListService : IToDoListService
{
    private readonly ToDoListDbContext context;
    private readonly IMapper mapper;

    public ToDoListService(ToDoListDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<Guid> CreateToDoListAsync(CreateToDoListDto createToDoListDto)
    {
        ToDoList toDoList = this.mapper.Map<ToDoList>(createToDoListDto);
        await this.context.ToDoLists.AddAsync(toDoList).ConfigureAwait(false);
        var result = await this.context.SaveChangesAsync().ConfigureAwait(false);
        if (result <= 0)
        {
            throw new ArgumentException("Failed to add ToDoList to the database.");
        }

        return toDoList.Id;
    }

    public async Task<GetToDoListDto> GetToDoListByIdAsync(Guid id)
    {
        var toDoList = await this.context.ToDoLists.
            Include(u => u.Tasks).
            FirstOrDefaultAsync(i => i.Id == id).ConfigureAwait(false);

        var userRoles = await this.context.ToDoListUsers.
            Where(i => i.ToDoListId == id).ToListAsync().ConfigureAwait(false);

        var result = this.mapper.Map<GetToDoListDto>(toDoList);
        var resultUserRoles = this.mapper.Map<List<ToDoListUserDto>>(userRoles);
        result.UserRoles = resultUserRoles;

        return result;
    }

    public Task<List<GetToDoListDto>> GetAllToDoListsAsync()
    {
        throw new NotImplementedException();
    }
}