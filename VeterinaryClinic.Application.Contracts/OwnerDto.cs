using VeterinaryClinic.Domain.Shared.Enums;
namespace VeterinaryClinic.Application.Contracts;

/// <summary>
/// Данные владельца
/// </summary>
public record OwnerDto(int Id, string LastName, string FirstName, string? Patronymic, string Phone, string Address);
/// <summary>
/// Данные для создания или обновления владельца
/// </summary>
public record SaveOwnerRequest(string LastName, string FirstName, string? Patronymic, string Phone, string Address);
