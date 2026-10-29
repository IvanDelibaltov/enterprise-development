using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Domain.Commands;

/// <summary>Данные для сохранения владельца</summary>
public sealed record OwnerCommand(string LastName, string FirstName, string? Patronymic, string Phone, string Address);
