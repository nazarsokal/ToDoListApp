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

    public async Task<List<GetToDoListDto>> GetAllToDoListsAsync(Guid userId)
    {
        var toDoLists = await this.context.ToDoLists
            .Where(td => td.UserRoles.Any(r => r.UserId == userId))
            .Include(u => u.Tasks)
            .ToListAsync()
            .ConfigureAwait(false);

        var result = this.mapper.Map<List<GetToDoListDto>>(toDoLists);

        // тягнемо всі ролі для цих списків
        var userRoles = await this.context.ToDoListUsers
                    .Where(r => toDoLists.Select(td => td.Id).Contains(r.ToDoListId))
                    .ToListAsync()
                    .ConfigureAwait(false);

        foreach (var dto in result)
        {
            var rolesForList = userRoles
                .Where(r => r.ToDoListId == dto.Id)
                .ToList();

            dto.UserRoles = this.mapper.Map<List<ToDoListUserDto>>(rolesForList);
        }

        return result;
    }

    public async Task<UpdateToDoListDto> UpdateToDoListAsync(Guid id, UpdateToDoListDto? updateToDoListDto)
    {
        var toDoList = this.context.ToDoLists.FirstOrDefault(t => t.Id == id);
        if (toDoList == null)
        {
            throw new ArgumentException($"ToDoList with id {id} not found.");
        }

        if (updateToDoListDto != null)
        {
            toDoList.Title = updateToDoListDto.Title;
            toDoList.Description = updateToDoListDto.Description;
            toDoList.Status = (TaskStatus)updateToDoListDto.Status;
        }

        this.context.ToDoLists.Update(toDoList);
        var result = await this.context.SaveChangesAsync().ConfigureAwait(false);

        if (result <= 0)
        {
            throw new ArgumentException("Failed to update ToDoList in the database.");
        }

        return updateToDoListDto!;
    }

    public async Task<AddUserToToDoListDto?> AddUserToToDoListAsync(Guid id, AddUserToToDoListDto? addUserToToDoListDto)
    {
        var toDoList = await this.context.ToDoLists
            .Include(t => t.UserRoles)
            .FirstOrDefaultAsync(t => t.Id == id).ConfigureAwait(false);

        ToDoListUser? existingUserRole = toDoList?.UserRoles
            .FirstOrDefault(ur => addUserToToDoListDto != null && ur.UserId == addUserToToDoListDto.UserId);

        if (existingUserRole == null)
        {
            {
                if (addUserToToDoListDto != null)
                {
                    var newUserRole = new ToDoListUser
                    {
                        ToDoListId = id,
                        UserId = addUserToToDoListDto.UserId,
                        Role = (UserRole)Enum.Parse(typeof(UserRole), addUserToToDoListDto.Role, true),
                    };
                    await this.context.ToDoListUsers.AddAsync(newUserRole).ConfigureAwait(false);
                }
            }

            var result = await this.context.SaveChangesAsync().ConfigureAwait(false);
            if (result <= 0)
            {
                throw new ArgumentException("Failed to add user to ToDoList in the database.");
            }

            return addUserToToDoListDto;
        }

        throw new ArgumentException("SOMEONE ALREADY HAS THIS ROLE IN THE LIST");
    }
}