using VeterinaryClinic.Domain.Shared.Enums;
namespace VeterinaryClinic.Application.Contracts;

public record OwnerDto(int Id, string LastName, string FirstName, string? Patronymic, string Phone, string Address);
public record SaveOwnerRequest(string LastName, string FirstName, string? Patronymic, string Phone, string Address);
